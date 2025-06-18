using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Services.Input;
using EndlessHeresy.Runtime.Services.Tick;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Movement
{
    public sealed class MovementInputController : PocoComponent
    {
        private readonly IGameUpdateService _gameUpdateService;
        private readonly IInputService _inputService;

        private MovementComponent _movementComponent;
        private FacingComponent _facingComponent;
        private Vector2 _inputValue;

        public MovementInputController(IGameUpdateService gameUpdateService, IInputService inputService)
        {
            _gameUpdateService = gameUpdateService;
            _inputService = inputService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
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

        private void OnFixedUpdate(float deltaTime) => _movementComponent.Move(_inputValue, deltaTime);

        private void OnUpdate(float deltaTime)
        {
            _inputValue = _inputService.GameplayActions.Movement.ReadValue<Vector2>();

            var isMoving = _inputValue != Vector2.zero;

            if (!isMoving)
            {
                return;
            }

            UpdateFacingDirection(_inputValue);
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