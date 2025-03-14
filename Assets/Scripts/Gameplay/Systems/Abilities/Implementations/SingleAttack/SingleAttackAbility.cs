using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Abilities.State;
using EndlessHeresy.Gameplay.Animations;
using EndlessHeresy.Gameplay.Attack;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Helpers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities.SingleAttack
{
    public sealed class SingleAttackAbility : Ability
    {
        private AnimationsStorageComponent _animationsStorage;
        private SingleAttackAnimation _singleAttackAnimation;
        private HealthComponent _selfHealthComponent;
        private MeleeAttackStorage _meleeAttackStorage;
        private FacingComponent _facingComponent;
        private DrawGizmosStorageComponent _drawGizmosStorage;

        private float _radius;
        private int _damage;
        private bool _isAttackFinished = true;
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
        }

        public override void Dispose()
        {
            base.Dispose();

            _singleAttackAnimation.OnAttackTriggered -= OnAttackTriggered;
            _drawGizmosStorage.OnDrawGizmosTriggered -= OnDrawGizmosTriggered;
        }

        public void SetRadius(float radius) => _radius = radius;

        public void SetDamage(int damage) => _damage = damage;

        public override async Task UseAsync(CancellationToken token)
        {
            PreAttack();
            await WaitForAttackTriggered(token);
            PostAttack();
        }

        private void PreAttack()
        {
            _isAttackFinished = false;
            SetState(AbilityState.InUse);
            _singleAttackAnimation.OnAttackTriggered += OnAttackTriggered;
            _singleAttackAnimation.OnAttackFinished += OnAttackFinished;
            _singleAttackAnimation.Play();
        }

        private void PostAttack()
        {
            _isAttackFinished = false;
            _singleAttackAnimation.OnAttackTriggered -= OnAttackTriggered;
            _singleAttackAnimation.OnAttackFinished -= OnAttackFinished;
            SetState(AbilityState.Ready);
        }

        private async Task WaitForAttackTriggered(CancellationToken token)
        {
            while (!_isAttackFinished)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                await Task.Yield();
            }
        }

        private void OnAttackFinished()
        {
            _isAttackFinished = true;
        }

        private void OnAttackTriggered()
        {
            var facingRight = _facingComponent.FacingRight;
            _attackPosition = _meleeAttackStorage.GetPosition(facingRight);
            var hasAttacked =
                PhysicsHelper.TryOverlapSphere(_attackPosition, _radius, out HealthComponent[] healthComponents);

            if (!hasAttacked)
            {
                return;
            }

            foreach (var healthComponent in healthComponents)
            {
                if (healthComponent == _selfHealthComponent)
                {
                    return;
                }

                healthComponent.TakeDamage(_damage);
            }
        }

        private void OnDrawGizmosTriggered()
        {
            if (_isAttackFinished)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_attackPosition, _radius);
            Gizmos.color = Color.white;
        }
    }
}