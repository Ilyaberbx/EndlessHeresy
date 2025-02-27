using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Services.Input;
using EndlessHeresy.Gameplay.Services.Tick;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class LocomotionState : BaseState<HeroActor>
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private MovementComponent _movementComponent;
        private AnimationsStorageComponent _animationsStorageComponent;
        private FacingComponent _facingComponent;
        private Animator _animator;

        private IGameUpdateService _gameUpdateService;
        private IInputService _inputService;

        [Inject]
        public void Construct(IGameUpdateService gameUpdateService,
            IInputService inputService)
        {
            _gameUpdateService = gameUpdateService;
            _inputService = inputService;
        }

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);

            _movementComponent = context.GetComponent<MovementComponent>();
            _facingComponent = context.GetComponent<FacingComponent>();
            _animationsStorageComponent = context.GetComponent<AnimationsStorageComponent>();
            _animator = _animationsStorageComponent.Animator;
        }

        public override Task EnterAsync(CancellationToken token)
        {
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
            var input = _inputService.GetMovementInput();
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
    }
}