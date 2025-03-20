using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Common;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Movement
{
    public sealed class MovementComponent : PocoComponent
    {
        private RigidbodyStorageComponent _rigidbodyStorage;

        private float _movementSpeed;
        private Transform _transform;
        private bool _isLocked;

        private Rigidbody2D Rigidbody => _rigidbodyStorage.Rigidbody;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnPostInitializeAsync(cancellationToken);

            _rigidbodyStorage = Owner.GetComponent<RigidbodyStorageComponent>();
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

            if (NoMovementInput(input))
            {
                Rigidbody.velocity = Vector2.zero;
                return;
            }

            var movement = CalculateRawMovement(input) * deltaTime;
            Rigidbody.AddForce(movement, ForceMode2D.Force);
        }

        private bool NoMovementInput(Vector2 input) => input == Vector2.zero;

        private Vector2 CalculateRawMovement(Vector2 input)
        {
            input.Normalize();
            return input * _movementSpeed;
        }
    }
}