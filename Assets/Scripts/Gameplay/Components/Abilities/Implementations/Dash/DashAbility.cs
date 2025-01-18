using System.Threading.Tasks;
using DG.Tweening;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Extensions;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Tags;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities.Dash
{
    public class DashAbility : Ability
    {
        private float _speed;
        private float _length;
        private AnimationCurve _curve;
        private HeroMovementComponent _movementComponent;
        private Tweener _dashTween;
        private ObstacleObserverComponent _obstacleObserverComponent;

        public void SetCurve(AnimationCurve curve) => _curve = curve;
        public void SetLength(float length) => _length = length;
        public void SetSpeed(float speed) => _speed = speed;

        protected override async Task CastAsync(IActor owner)
        {
            if (!owner.TryGetComponent(out _movementComponent))
            {
                return;
            }

            if (!owner.TryGetComponent(out _obstacleObserverComponent))
            {
                return;
            }

            var camera = Camera.main;

            if (camera == null)
            {
                return;
            }

            var mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            var ownerTransform = owner.GameObject.transform;
            var lookDirection = ownerTransform.position.DirectionTo(mouseWorldPosition).ToVector2();
            var endValue = ownerTransform.position.ToVector2() + lookDirection * _length;

            _dashTween = GetDashTween(owner.GameObject, ownerTransform, endValue);

            PreCast();
            await _dashTween.AsTask(CastCancellationToken);
            _dashTween = null;
            PostCast();
        }

        private void PreCast()
        {
            _movementComponent.Lock();
            _obstacleObserverComponent.OnTriggerEnter += OnObstacleEntered;
            SetStatus(AbilityStatus.InUse);
        }

        private void PostCast()
        {
            _obstacleObserverComponent.OnTriggerEnter -= OnObstacleEntered;
            SetStatus(AbilityStatus.Ready);
            _movementComponent.Unlock();
        }

        private Tweener GetDashTween(GameObject gameObject, Transform ownerTransform, Vector2 endValue)
        {
            return ownerTransform.DOMove(endValue, _speed)
                .SetEase(_curve)
                .SetSpeedBased(true)
                .SetId(gameObject);
        }

        private void OnObstacleEntered(ObstacleTagComponent obstacleTagComponent)
        {
            _dashTween?.Kill();
            PostCast();
        }
    }
}