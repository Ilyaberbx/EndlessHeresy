using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Movement
{
    public sealed class MovementComponent : PocoComponent
    {
        private float _movementSpeed;
        private Transform _transform;
        private Vector2 _movementInput;
        private bool _isLocked;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnPostInitializeAsync(cancellationToken);

            _transform = Owner.GameObject.transform;
        }

        public void SetSpeed(float movementSpeed) => _movementSpeed = movementSpeed;
        public void Lock() => _isLocked = true;
        public void Unlock() => _isLocked = false;

        public void Move(Vector2 input, float deltaTime)
        {
            if (_isLocked)
            {
                return;
            }

            _movementInput = input;

            if (NoMovementInput())
            {
                return;
            }

            var movement = CalculateRawMovement() * deltaTime;

            _transform.Translate(movement);
            _movementInput = Vector2.zero;
        }

        private bool NoMovementInput() => _movementInput == Vector2.zero;

        private Vector2 CalculateRawMovement()
        {
            _movementInput.Normalize();
            return _movementInput * _movementSpeed;
        }
    }
}