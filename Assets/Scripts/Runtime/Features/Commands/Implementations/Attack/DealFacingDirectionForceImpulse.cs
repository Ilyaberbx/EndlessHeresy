using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Generic;
using UnityEngine;

namespace EndlessHeresy.Runtime.Commands.Attack
{
    public sealed class DealFacingDirectionForceImpulse : ICommand
    {
        private readonly float _multiplier;

        public DealFacingDirectionForceImpulse(float multiplier)
        {
            _multiplier = multiplier;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            if (!actor.TryGetComponent<RigidbodyStorageComponent>(out var rigidbodyStorageComponent))
            {
                return Task.CompletedTask;
            }
            
            var facingComponent = actor.GetComponent<FacingComponent>();
            var horizontalDirection = facingComponent.IsFacingRight ? 1f : -1f;
            var forceDirection = new Vector2(horizontalDirection, 0);
            var force = forceDirection * _multiplier;
            var rigidbody = rigidbodyStorageComponent.Rigidbody;
            rigidbody.AddForce(force, ForceMode2D.Impulse);
            return Task.CompletedTask;
        }
    }
}