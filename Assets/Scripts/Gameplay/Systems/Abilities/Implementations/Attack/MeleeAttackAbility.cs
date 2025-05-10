using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Operational;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.Utilities;
using EndlessHeresy.Gameplay.Utilities.Gizmos;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class MeleeAttackAbility : AbilityWithCooldown
    {
        private HealthComponent _selfHealthComponent;
        private DrawGizmosObserver _drawGizmosObserver;
        private ProcessAttackQuery? _currentProcessAttackQuery;
        private MouseFacingComponent _mouseFacingComponent;

        protected FacingComponent FacingComponent => _mouseFacingComponent.FacingComponent;


        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);
            _selfHealthComponent = Owner.GetComponent<HealthComponent>();
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

        protected void ProcessAttack(ProcessAttackQuery query)
        {
            _currentProcessAttackQuery = query;

            var hasAttacked = GamePhysicsUtility.TryOverlapCapsuleAll<HealthComponent>(
                query.OverlapData,
                query.At,
                out var healthComponents);

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

                healthComponent.TakeDamage(query.DamageData);


                if (healthComponent.IsDead())
                {
                    continue;
                }

                var owner = healthComponent.Owner;

                if (owner.TryGetComponent<StatusEffectsComponent>(out var statusEffectsComponent))
                {
                    statusEffectsComponent.Add(StatusEffectType.Burning);
                }

                ProcessForceAt(query.At, healthComponent.Owner, query.Force);
            }
        }

        protected void ProcessOwnerFacingForce(float forceMultiplier)
        {
            var horizontalDirection = FacingComponent.IsFacingRight ? 1f : -1f;
            var forceDirection = new Vector2(horizontalDirection, 0);
            var force = forceDirection * forceMultiplier;
            ProcessForce(Owner, force);
        }

        protected ProcessAttackQuery CollectProcessAttackQuery(AttackData data, Vector2 at)
        {
            var offSet = data.OffSet;

            var processedOffSet = Vector2
                .zero
                .AddX(FacingComponent.IsFacingRight ? offSet.x : -offSet.x)
                .AddY(offSet.y)
                .ToVector2();

            return new ProcessAttackQuery(data.DamageData,
                data.Force,
                data.OverlapData,
                at + processedOffSet);
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
                Gizmos.color = Color.white;
                return;
            }

            if (!_currentProcessAttackQuery.HasValue)
            {
                return;
            }

            var data = _currentProcessAttackQuery.Value;
            GizmosUtility.DrawWireCapsule2D(data.OverlapData, data.At, Color.yellow);
        }
    }
}