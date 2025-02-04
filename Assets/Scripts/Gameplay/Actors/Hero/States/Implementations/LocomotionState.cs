using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Services.Tick;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class LocomotionState : HeroState
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private MovementComponent _movementComponent;
        private AnimatorComponent _animatorComponent;
        private FacingComponent _facingComponent;
        private Animator _animator;

        private IGameUpdateService _gameUpdateService;

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);

            TryCollectRequiredComponents();
            _gameUpdateService = ServiceLocator.Get<GameUpdateService>();
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _animator = _animatorComponent.Animator;
            _movementComponent.Unlock();
            _gameUpdateService.OnUpdate += OnUpdate;
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _movementComponent.Lock();
            _gameUpdateService.OnUpdate -= OnUpdate;
            return Task.CompletedTask;
        }

        private void OnUpdate(float deltaTime)
        {
            var input = Context.GetMovementInput();
            var isMoving = input != Vector2.zero;
            UpdateAnimatorState(isMoving);

            if (!isMoving)
            {
                return;
            }

            UpdateFacingDirection(input);
            _movementComponent.Move(input, deltaTime);
        }

        private void UpdateFacingDirection(Vector2 input)
        {
            if (input.x == 0)
            {
                return;
            }

            var isFacingRight = input.x > 0;
            _facingComponent.Face(isFacingRight);
        }

        private void UpdateAnimatorState(bool isMoving) => _animator.SetBool(IsMoving, isMoving);

        private bool TryCollectRequiredComponents() =>
            Context.TryGetComponent(out _movementComponent) &&
            Context.TryGetComponent(out _animatorComponent) &&
            Context.TryGetComponent(out _facingComponent);
    }
}