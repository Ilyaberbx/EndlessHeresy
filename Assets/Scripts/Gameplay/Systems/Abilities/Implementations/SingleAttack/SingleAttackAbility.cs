using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Gameplay.Animations;
using EndlessHeresy.Gameplay.Attack;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Helpers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities.SingleAttack
{
    public sealed class SingleAttackAbility : AbilityWithCooldown
    {
        private AnimationsStorageComponent _animationsStorage;
        private SingleAttackAnimation _singleAttackAnimation;
        private HealthComponent _selfHealthComponent;
        private MeleeAttackStorage _meleeAttackStorage;
        private FacingComponent _facingComponent;
        private DrawGizmosStorageComponent _drawGizmosStorage;

        private float _radius;
        private int _damage;
        private int _force;
        private bool _isAttackFinished;
        private Vector2 _attackPosition;

        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);
            _animationsStorage = Owner.GetComponent<AnimationsStorageComponent>();
            _selfHealthComponent = Owner.GetComponent<HealthComponent>();
            _meleeAttackStorage = Owner.GetComponent<MeleeAttackStorage>();
            _facingComponent = Owner.GetComponent<FacingComponent>();
            _drawGizmosStorage = Owner.GetComponent<DrawGizmosStorageComponent>();
            _animationsStorage.TryGetAnimation(out _singleAttackAnimation);
            _drawGizmosStorage.OnDrawGizmosTriggered += OnDrawGizmosTriggered;
            _isAttackFinished = true;
        }

        public override void Dispose()
        {
            base.Dispose();
            _singleAttackAnimation.OnAttackTriggered -= OnAttackTriggered;
            _drawGizmosStorage.OnDrawGizmosTriggered -= OnDrawGizmosTriggered;
        }

        public void SetRadius(float radius) => _radius = radius;
        public void SetDamage(int damage) => _damage = damage;
        public void SetForce(int force) => _force = force;

        public override async Task UseAsync(CancellationToken token)
        {
            _isAttackFinished = false;
            SetState(AbilityState.InUse);
            _singleAttackAnimation.OnAttackTriggered += OnAttackTriggered;
            _singleAttackAnimation.OnAttackFinished += OnAttackFinished;
            _singleAttackAnimation.Play();
            await WaitForAttackTriggered(token);
            _singleAttackAnimation.OnAttackTriggered -= OnAttackTriggered;
            _singleAttackAnimation.OnAttackFinished -= OnAttackFinished;
            SetState(AbilityState.Cooldown);
        }

        private async Task WaitForAttackTriggered(CancellationToken token)
        {
            while (!_isAttackFinished)
            {
                if (token.IsCancellationRequested) return;
                await Task.Yield();
            }
        }

        private void OnAttackFinished() => _isAttackFinished = true;

        private void OnAttackTriggered()
        {
            _attackPosition = _meleeAttackStorage.GetPosition(_facingComponent.FacingRight);

            if (!PhysicsHelper.TryOverlapCircleAll(_attackPosition, _radius, out HealthComponent[] healthComponents))
                return;

            foreach (var healthComponent in healthComponents)
            {
                if (healthComponent == _selfHealthComponent) continue;
                healthComponent.TakeDamage(_damage);
                TryApplyPhysicalForce(healthComponent);
            }
        }

        private bool TryApplyPhysicalForce(HealthComponent healthComponent)
        {
            if (healthComponent.IsDead()) return false;
            var hasRigidbody = healthComponent.Owner.TryGetComponent(out RigidbodyStorageComponent rigidbodyStorage);
            
            if (!hasRigidbody) return false;
            var forceDirection = (healthComponent.Owner.Transform.position.ToVector2() - _attackPosition) * _force;
            rigidbodyStorage.Rigidbody.velocity = forceDirection;
            return true;
        }

        private void OnDrawGizmosTriggered()
        {
            if (_isAttackFinished) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_attackPosition, _radius);
            Gizmos.color = Color.white;
        }
    }
}