using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Generic;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Movement
{
    public sealed class MovementComponent : PocoComponent
    {
        private const float FullStopMagnitude = 1f;
        private RigidbodyStorageComponent _rigidbodyStorage;
        private StatsComponent _statsComponent;

        private Stat _moveSpeedStat;
        private Transform _transform;
        private bool _isLocked;

        private Rigidbody2D Rigidbody => _rigidbodyStorage.Rigidbody;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _rigidbodyStorage = Owner.GetComponent<RigidbodyStorageComponent>();
            _statsComponent = Owner.GetComponent<StatsComponent>();
            _moveSpeedStat = _statsComponent.GetStat(StatType.MoveSpeed);
            return Task.CompletedTask;
        }

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
                if (Rigidbody.linearVelocity.magnitude > FullStopMagnitude)
                {
                    return;
                }

                Rigidbody.linearVelocity = Vector2.zero;

                return;
            }

            var movement = CalculateRawMovement(input) * deltaTime;
            Rigidbody.AddForce(movement, ForceMode2D.Force);
        }

        private bool NoMovementInput(Vector2 input) => input == Vector2.zero;

        private Vector2 CalculateRawMovement(Vector2 input)
        {
            input.Normalize();
            return input * _moveSpeedStat.ProcessedValueProperty.Value;
        }
    }
}