using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Generic;
using UnityEngine;

namespace EndlessHeresy.Runtime.Commands.Attack
{
    public sealed class DealExplosionForceImpulse : ICommand
    {
        private readonly Vector2 _at;
        private readonly float _multiplier;

        public DealExplosionForceImpulse(Vector2 at, float multiplier)
        {
            _at = at;
            _multiplier = multiplier;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            if (!actor.TryGetComponent<RigidbodyStorageComponent>(out var rigidbodyStorageComponent))
            {
                return Task.CompletedTask;
            }

            var rigidbody = rigidbodyStorageComponent.Rigidbody;
            var actorPosition = actor.Transform.position;
            var forceDirection = _at.DirectionTo(actorPosition).normalized;
            var processedForce = forceDirection * _multiplier;

            rigidbody.AddForce(processedForce, ForceMode2D.Impulse);
            return Task.CompletedTask;
        }
    }
}