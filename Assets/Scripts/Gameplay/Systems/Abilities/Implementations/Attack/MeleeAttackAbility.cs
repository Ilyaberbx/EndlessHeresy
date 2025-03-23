using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Gameplay.Attack;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Data.Operational;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Helpers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class MeleeAttackAbility : AbilityWithCooldown
    {
        private MeleeAttackStorage _attackPointsStorage;
        private HealthComponent _selfHealthComponent;
        private DrawGizmosObserver _drawGizmosObserver;
        private ProcessAttackDto _currentProcessAttackDto;
        private MouseFacingComponent _mouseFacingComponent;
        protected FacingComponent FacingComponent => _mouseFacingComponent.FacingComponent;

        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);
            _selfHealthComponent = Owner.GetComponent<HealthComponent>();
            _attackPointsStorage = Owner.GetComponent<MeleeAttackStorage>();
            _mouseFacingComponent = Owner.GetComponent<MouseFacingComponent>();
            _drawGizmosObserver = Owner.GetComponent<DrawGizmosObserver>();
            _drawGizmosObserver.OnDrawGizmosTriggered += OnDrawGizmosTriggered;
        }

        public override void Dispose()
        {
            base.Dispose();
            _drawGizmosObserver.OnDrawGizmosTriggered -= OnDrawGizmosTriggered;
        }

        public override Task UseAsync(CancellationToken token)
        {
            _mouseFacingComponent.UpdateFacing();
            return Task.CompletedTask;
        }

        protected void ProcessAttack(ProcessAttackDto dto)
        {
            _currentProcessAttackDto = dto;
            var at = _attackPointsStorage.GetPosition(FacingComponent.IsFacingRight);
            var hasAttacked =
                GamePhysics.TryOverlapCircleAll<HealthComponent>(at, dto.Radius, out var healthComponents);

            if (!hasAttacked)
            {
                return;
            }

            foreach (var healthComponent in healthComponents)
            {
                if (healthComponent == _selfHealthComponent)
                {
                    continue;
                }

                healthComponent.TakeDamage(dto.Damage);

                if (healthComponent.IsDead())
                {
                    continue;
                }

                ProcessForceAt(at, healthComponent.Owner, dto.Force);
            }
        }

        protected void ProcessOwnerFacingForce(float forceMultiplier)
        {
            var horizontalDirection = FacingComponent.IsFacingRight ? 1f : -1f;
            var forceDirection = new Vector2(horizontalDirection, 0);
            var force = forceDirection * forceMultiplier;
            ProcessForce(Owner, force);
        }

        protected ProcessAttackDto CollectProcessAttackDto(AttackData data)
        {
            return new ProcessAttackDto(data.Damage,
                data.Force,
                data.Radius,
                data.DragForce);
        }

        private void ProcessForceAt(Vector2 at, IActor actor, float forceMultiplier)
        {
            var actorPosition = actor.Transform.position;
            var forceDirection = at.DirectionTo(actorPosition).normalized;
            var processedForce = forceDirection * forceMultiplier;

            ProcessForce(actor, processedForce);
        }

        private void ProcessForce(IActor actor, Vector2 force)
        {
            if (!actor.TryGetComponent<RigidbodyStorageComponent>(out var rigidbodyStorageComponent))
            {
                return;
            }

            var rigidbody = rigidbodyStorageComponent.Rigidbody;
            rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        private void OnDrawGizmosTriggered()
        {
            if (State.Value != AbilityState.InUse)
            {
                return;
            }

            var at = _attackPointsStorage.GetPosition(FacingComponent.IsFacingRight);
            var radius = _currentProcessAttackDto.Radius;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(at, radius);
            Gizmos.color = Color.white;
        }
    }
}