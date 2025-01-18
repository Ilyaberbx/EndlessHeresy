using System.Threading.Tasks;
using DG.Tweening;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Extensions;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Tags;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities.Dash
{
    public class DashAbility : Ability
    {
        private float _speed;
        private float _length;
        private float _damage;
        private AnimationCurve _curve;

        private Tweener _dashTween;
        private HeroMovementComponent _movementComponent;
        private ObstacleTriggerObserver _obstacleTriggerObserver;
        private EnemyTriggerObserver _enemyTriggerObserver;

        public void SetCurve(AnimationCurve curve) => _curve = curve;
        public void SetLength(float length) => _length = length;
        public void SetSpeed(float speed) => _speed = speed;
        public void SetDamage(float damage) => _damage = damage;

        protected override async Task CastAsync(IActor owner)
        {
            if (!GetRequiredComponents(owner))
                return;

            if (!TryInitializeTween(owner))
                return;

            PrepareForDash();
            await _dashTween.AsTask(CastCancellationToken);
            CompleteDash();
        }

        private bool IsComponentsInitialized() => _movementComponent != null && _obstacleTriggerObserver != null;

        private void ObstacleTriggerEntered(ObstacleTagComponent obstacleTagComponent)
        {
            if (_dashTween == null)
            {
                return;
            }

            _dashTween.Kill();
            CompleteDash();
        }

        private bool GetRequiredComponents(IActor owner)
        {
            var componentsInitialized = IsComponentsInitialized();
            if (componentsInitialized)
            {
                return true;
            }

            return owner.TryGetComponent(out _movementComponent) &&
                   owner.TryGetComponent(out _obstacleTriggerObserver) &&
                   owner.TryGetComponent(out _enemyTriggerObserver);
        }

        private bool TryInitializeTween(IActor owner)
        {
            var camera = Camera.main;
            if (camera == null)
            {
                _dashTween = null;
                return false;
            }

            var mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            var ownerTransform = owner.GameObject.transform;
            var direction = ownerTransform.position.DirectionTo(mouseWorldPosition).ToVector2();
            var endPosition = ownerTransform.position.ToVector2() + direction * _length;
            _dashTween = BuildDashTween(owner.GameObject, ownerTransform, endPosition);
            return true;
        }

        private Tweener BuildDashTween(GameObject gameObject, Transform ownerTransform, Vector2 endValue) =>
            ownerTransform.DOMove(endValue, _speed)
                .SetEase(_curve)
                .SetSpeedBased(true)
                .SetId(gameObject);

        private void PrepareForDash()
        {
            _movementComponent.Lock();
            _obstacleTriggerObserver.OnTriggerEnter += ObstacleTriggerEntered;
            _enemyTriggerObserver.OnTriggerEnter += OnEnemyTriggerEnter;
            SetStatus(AbilityStatus.InUse);
        }

        private void OnEnemyTriggerEnter(EnemyTagComponent enemyTagComponent)
        {
            var enemy = enemyTagComponent.Owner;
            if (!enemy.TryGetComponent(out HealthComponent healthComponent))
            {
                return;
            }

            healthComponent.TakeDamage(_damage);
        }

        private void CompleteDash()
        {
            _movementComponent.Unlock();
            _obstacleTriggerObserver.OnTriggerEnter -= ObstacleTriggerEntered;
            SetStatus(AbilityStatus.Ready);
        }
    }
}