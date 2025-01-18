using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.Global.Services.Input;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Movement
{
    public sealed class HeroMovementComponent : PocoComponent
    {
        private float _movementSpeed;
        private InputService _inputService;
        private GameUpdateService _gameUpdateService;
        private Transform _transform;
        private Vector2 _movementInput;
        private bool _isLocked;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnPostInitializeAsync(cancellationToken);

            _transform = Owner.GameObject.transform;
            _inputService = ServiceLocator.Get<InputService>();
            _gameUpdateService = ServiceLocator.Get<GameUpdateService>();

            _gameUpdateService.OnUpdate += OnUpdate;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _gameUpdateService.OnUpdate -= OnUpdate;
        }

        public void Configure(float movementSpeed) => _movementSpeed = movementSpeed;

        private void OnUpdate(float deltaTime)
        {
            if (_isLocked)
            {
                return;
            }

            UpdateMovementInput();
            if (NoMovementInput())
            {
                return;
            }

            var movement = CalculateRawMovement() * deltaTime;
            Move(movement);
        }

        private Vector2 CalculateRawMovement()
        {
            if (NoMovementInput())
            {
                return Vector2.zero;
            }

            _movementInput.Normalize();
            return _movementInput * _movementSpeed;
        }

        public void Lock() => _isLocked = true;
        public void Unlock() => _isLocked = false;
        private void Move(Vector2 movement) => _transform.Translate(movement);
        private void UpdateMovementInput() => _movementInput = _inputService.GetMovementInput();
        private bool NoMovementInput() => _movementInput == Vector2.zero;
    }
}