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
        private Vector2 _input;

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
            _facingComponent.Lock(GetType());
            _gameUpdateService.OnUpdate += OnUpdate;
            _gameUpdateService.OnFixedUpdate += OnFixedUpdate;
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _movementComponent.Lock();
            _facingComponent.Unlock(GetType());
            _gameUpdateService.OnUpdate -= OnUpdate;
            _gameUpdateService.OnFixedUpdate -= OnFixedUpdate;
            return Task.CompletedTask;
        }

        private void OnFixedUpdate(float deltaTime) => _movementComponent.Move(_input, deltaTime);

        private void OnUpdate(float deltaTime)
        {
            _input = _inputService.GetMovementInput();
            var isMoving = _input != Vector2.zero;
            UpdateAnimatorState(isMoving);

            if (!isMoving)
            {
                return;
            }

            UpdateFacingDirection(_input);
        }

        private void UpdateFacingDirection(Vector2 input)
        {
            if (input.x == 0)
            {
                return;
            }

            var isFacingRight = input.x > 0;
            _facingComponent.Face(GetType(), isFacingRight);
        }

        private void UpdateAnimatorState(bool isMoving) => _animator.SetBool(IsMoving, isMoving);
    }
}