using DG.Tweening;
using EndlessHeresy.Gameplay.Extensions;
using EndlessHeresy.Gameplay.Hero;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities.Dash
{
    public class DashAbility : Ability
    {
        private float _speed;
        private float _length;
        private AnimationCurve _curve;

        private HeroMovementComponent _movementComponent;
        public void SetCurve(AnimationCurve curve) => _curve = curve;
        public void SetLength(float length) => _length = length;
        public void SetSpeed(float speed) => _speed = speed;

        protected override void Cast(GameObject owner)
        {
            var ownerTransform = owner.transform;
            var lookComponent = owner.GetComponent<LookComponent>();
            _movementComponent = owner.GetComponent<HeroMovementComponent>();

            _movementComponent.enabled = false;
            var lookDirection = lookComponent.NormalizedLookDirection;

            var endValue = ownerTransform.position.ToVector2() + lookDirection * _length;

            ownerTransform.DOMove(endValue, _speed)
                .SetEase(_curve)
                .SetSpeedBased(true)
                .SetId(owner)
                .OnComplete(OnDashComplete);

            SetStatus(AbilityStatus.Cooldown);
        }

        private void OnDashComplete()
        {
            SetStatus(AbilityStatus.Ready);
            _movementComponent.enabled = true;
        }
    }
}