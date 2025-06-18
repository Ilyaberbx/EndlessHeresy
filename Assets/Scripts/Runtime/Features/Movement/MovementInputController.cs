using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Input;
using EndlessHeresy.Runtime.Services.Tick;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Movement
{
    public sealed class MovementInputController : PocoComponent
    {
        private IGameUpdateService _gameUpdateService;

        private MovementComponent _movementComponent;
        private FacingComponent _facingComponent;
        private Vector2 _input;
        private GameplayInputStorage _gameplayInputStorage;

        private GameActions GameActions => _gameplayInputStorage.GameActions;

        [Inject]
        public void Construct(IGameUpdateService gameUpdateService)
        {
            _gameUpdateService = gameUpdateService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _gameplayInputStorage = Owner.GetComponent<GameplayInputStorage>();
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
            _input = GameActions.Movement.Movement.ReadValue<Vector2>();

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