using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Attack;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Data.Components;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Helpers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class MeleeAttackAbility : AbilityWithCooldown
    {
        private MeleeAttackStorage _attackPointsStorage;
        private FacingComponent _faceDirectionStorage;
        private HealthComponent _selfHealthComponent;

        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);
            _selfHealthComponent = Owner.GetComponent<HealthComponent>();
            _attackPointsStorage = Owner.GetComponent<MeleeAttackStorage>();
            _faceDirectionStorage = Owner.GetComponent<FacingComponent>();
        }

        protected void ProcessAttack(AttackData data)
        {
            var at = _attackPointsStorage.GetPosition(_faceDirectionStorage.IsFacingRight);
            var hasAttacked =
                GamePhysics.TryOverlapCircleAll<HealthComponent>(at, data.Radius, out var healthComponents);

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

                healthComponent.TakeDamage(data.Damage);

                if (healthComponent.IsDead())
                {
                    continue;
                }

                ProcessForceAt(at, healthComponent.Owner, data.Force);
            }
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

        protected void ProcessOwnerFacingForce(float forceMultiplier)
        {
            var horizontalDirection = _faceDirectionStorage.IsFacingRight ? 1f : -1f;
            var forceDirection = new Vector2(horizontalDirection, 0);
            var force = forceDirection * forceMultiplier;
            ProcessForce(Owner, force);
        }
    }
}