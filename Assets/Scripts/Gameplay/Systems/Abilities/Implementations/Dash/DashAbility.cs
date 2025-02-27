using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using EndlessHeresy.Core;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Abilities.State;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Services.Camera;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    public sealed class DashAbility : Ability
    {
        private ICameraService _cameraService;

        private MovementComponent _movementComponent;
        private AnimationCurve _curve;
        private float _length;
        private int _speed;
        private FacingComponent _facingComponent;

        [Inject]
        public void Construct(ICameraService cameraService) => _cameraService = cameraService;

        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);
            _movementComponent = Owner.GetComponent<MovementComponent>();
            _facingComponent = Owner.GetComponent<FacingComponent>();
        }

        public override async Task UseAsync(CancellationToken token)
        {
            var camera = _cameraService.MainCamera;
            var mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            var lookDirection = Owner.Transform.position.DirectionTo(mouseWorldPosition).ToVector2();
            PreDash(lookDirection);
            await ExecuteAsync(token, lookDirection);
            PostDash();
        }

        public void SetCurve(AnimationCurve curve) => _curve = curve;
        public void SetLength(float length) => _length = length;
        public void SetSpeed(int speed) => _speed = speed;

        private Task ExecuteAsync(CancellationToken token, Vector2 lookDirection)
        {
            var ownerTransform = Owner.Transform;
            var endValue = ownerTransform.position.ToVector2() + lookDirection * _length;
            var tween = GetDashTween(Owner.GameObject, ownerTransform, endValue);
            return tween.AsTask(token);
        }

        private void PreDash(Vector2 lookDirection)
        {
            _movementComponent.Lock();
            _facingComponent.Face(lookDirection.x > 0);
            SetState(AbilityState.InUse);
        }

        private void PostDash()
        {
            _movementComponent.Unlock();
            SetState(AbilityState.Ready);
        }

        private Tweener GetDashTween(GameObject gameObject, Transform ownerTransform, Vector2 endValue) =>
            ownerTransform.DOMove(endValue, _speed)
                .SetEase(_curve)
                .SetSpeedBased(true)
                .SetId(gameObject);
    }
}