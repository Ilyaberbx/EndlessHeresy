using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Stats.Modifiers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Movement
{
    public sealed class MovementComponent : PocoComponent
    {
        private RigidbodyStorageComponent _rigidbodyStorage;
        private StatModifiersComponent _statModifiersComponent;

        private ReadOnlyReactiveProperty<int> _moveSpeedStat;
        private Transform _transform;
        private bool _isLocked;

        private Rigidbody2D Rigidbody => _rigidbodyStorage.Rigidbody;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _rigidbodyStorage = Owner.GetComponent<RigidbodyStorageComponent>();
            _statModifiersComponent = Owner.GetComponent<StatModifiersComponent>();
            _moveSpeedStat = _statModifiersComponent.GetProcessedStat(StatType.MoveSpeed);
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
            return input * _moveSpeedStat.Value;
        }
    }
}