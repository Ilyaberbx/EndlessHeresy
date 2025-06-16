using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Generic;
using EndlessHeresy.Runtime.Services.Input;
using EndlessHeresy.Runtime.Services.Tick;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Movement
{
    public sealed class InputMovementComponent : PocoComponent
    {
        private IGameUpdateService _gameUpdateService;
        private IInputService _inputService;

        private AnimatorStorageComponent _animatorStorage;
        private MovementComponent _movementComponent;
        private FacingComponent _facingComponent;
        private Vector2 _input;

        [Inject]
        public void Construct(IGameUpdateService gameUpdateService,
            IInputService inputService)
        {
            _gameUpdateService = gameUpdateService;
            _inputService = inputService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _animatorStorage = Owner.GetComponent<AnimatorStorageComponent>();
            _movementComponent = Owner.GetComponent<MovementComponent>();
            _facingComponent = Owner.GetComponent<FacingComponent>();
            _gameUpdateService.OnUpdate += OnUpdate;
            _gameUpdateService.OnFixedUpdate += OnFixedUpdate;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _gameUpdateService.OnUpdate -= OnUpdate;
            _gameUpdateService.OnFixedUpdate -= OnFixedUpdate;
        }

        private void OnFixedUpdate(float deltaTime) => _movementComponent.Move(_input, deltaTime);

        private void OnUpdate(float deltaTime)
        {
            _input = _inputService.GetMovementInput();
            var isMoving = _input != Vector2.zero;

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
    }
}