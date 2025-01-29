using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
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
    public class Dash : Ability
    {
        private float _speed;
        private float _length;
        private float _damage;
        private AnimationCurve _curve;

        private readonly IList<HealthComponent> _attachedHealthComponents = new List<HealthComponent>();
        private HeroMovementComponent _movementComponent;
        private ObstacleTriggerObserver _obstacleTriggerObserver;
        private EnemyTriggerObserver _enemyTriggerObserver;
        private Tweener _dashTween;

        public void Configure(float speed,
            float length,
            float damage,
            AnimationCurve curve)
        {
            _speed = speed;
            _length = length;
            _damage = damage;
            _curve = curve;
        }

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

        private void OnObstacleTriggerEntered(ObstacleTagComponent obstacleTagComponent)
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

            var ownerTransform = owner.GameObject.transform;
            var endPosition = GetEndPosition(camera, ownerTransform);
            _dashTween = BuildDashTween(owner.GameObject, ownerTransform, endPosition);
            return true;
        }

        private Vector2 GetEndPosition(Camera camera, Transform ownerTransform)
        {
            var mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            var ownerPosition = ownerTransform.position.ToVector2();
            var direction = ownerPosition.DirectionTo(mouseWorldPosition);
            var endPosition = ownerPosition + direction * _length;
            return endPosition;
        }

        private Tweener BuildDashTween(GameObject gameObject, Transform ownerTransform, Vector2 endValue) =>
            ownerTransform.DOMove(endValue, _speed)
                .SetEase(_curve)
                .SetSpeedBased(true)
                .SetId(gameObject);

        private void PrepareForDash()
        {
            _movementComponent.Lock();
            _obstacleTriggerObserver.OnTriggerEnter += OnObstacleTriggerEntered;
            _enemyTriggerObserver.OnTriggerEnter += OnEnemyTriggerEntered;
            SetStatus(AbilityStatus.InUse);
        }

        private void OnEnemyTriggerEntered(EnemyTagComponent enemyTagComponent)
        {
            var enemy = enemyTagComponent.Owner;
            if (!enemy.TryGetComponent(out HealthComponent healthComponent))
            {
                return;
            }

            if (_attachedHealthComponents.Contains(healthComponent))
            {
                return;
            }

            healthComponent.TakeDamage(_damage);
            _attachedHealthComponents.Add(healthComponent);
        }

        private void CompleteDash()
        {
            _movementComponent.Unlock();
            _attachedHealthComponents.Clear();
            _obstacleTriggerObserver.OnTriggerEnter -= OnObstacleTriggerEntered;
            _enemyTriggerObserver.OnTriggerEnter -= OnEnemyTriggerEntered;
            SetStatus(AbilityStatus.Ready);
        }
    }
}