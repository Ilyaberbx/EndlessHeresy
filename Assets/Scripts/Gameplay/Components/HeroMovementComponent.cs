using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.Update;
using EndlessHeresy.Global.Services.Input;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Hero
{
    public sealed class HeroMovementComponent : MonoBehaviour, IGameUpdatable
    {
        [SerializeField] private float _movementSpeed;

        private InputService _inputService;
        private GameUpdateService _gameUpdateService;

        private Vector2 _movementInput;

        private void Start()
        {
            _inputService = ServiceLocator.Get<InputService>();
            _gameUpdateService = ServiceLocator.Get<GameUpdateService>();

            _gameUpdateService.Subscribe(this);
        }

        private void OnDestroy()
        {
            _gameUpdateService.Unsubscribe(this);
        }

        public void OnUpdate(float deltaTime)
        {
            UpdateMovementInput();
            if (NoMovementInput())
            {
                return;
            }

            var movement = CalculateMovement(deltaTime);
            Move(movement);
        }

        private Vector2 CalculateMovement(float deltaTime)
        {
            _movementInput.Normalize();
            var rawMovement = _movementInput * _movementSpeed;
            var movement = rawMovement * deltaTime;
            return movement;
        }

        private void Move(Vector2 movement) => transform.Translate(movement);
        private void UpdateMovementInput() => _movementInput = _inputService.GetMovementInput();
        private bool NoMovementInput() => _movementInput == Vector2.zero;
    }
}