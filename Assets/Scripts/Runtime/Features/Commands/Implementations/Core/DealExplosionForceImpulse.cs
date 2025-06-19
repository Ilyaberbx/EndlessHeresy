using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Generic;
using EndlessHeresy.Runtime.Utilities;
using UnityEngine;

namespace EndlessHeresy.Runtime.Commands.Core
{
    public sealed class DealExplosionForceImpulse : ICommand
    {
        private readonly Vector2 _offSet;
        private readonly CapsuleOverlapData _data;
        private readonly float _multiplier;

        public DealExplosionForceImpulse(Vector2 offSet, CapsuleOverlapData data, float multiplier)
        {
            _offSet = offSet;
            _data = data;
            _multiplier = multiplier;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            if (!actor.TryGetComponent<FacingComponent>(out var facingComponent))
            {
                return Task.CompletedTask;
            }

            var selfRigidbody = actor.GetComponent<RigidbodyStorageComponent>().Rigidbody;

            var ownerPosition = actor.Transform.position;
            var at = ownerPosition
                .AddX(facingComponent.IsFacingRight ? _offSet.x : -_offSet.x)
                .AddY(_offSet.y)
                .ToVector2();

            var hasAttacked = GamePhysicsUtility.TryOverlapCapsuleAll<RigidbodyStorageComponent>(
                _data,
                at,
                out var rigidbodyStorages);

            if (!hasAttacked)
            {
                return Task.CompletedTask;
            }

            foreach (var rigidbodyStorage in rigidbodyStorages)
            {
                var rigidbody = rigidbodyStorage.Rigidbody;
                if (rigidbody == selfRigidbody)
                {
                    continue;
                }

                var actorPosition = rigidbodyStorage.Owner.Transform.position;
                var forceDirection = at.DirectionTo(actorPosition).normalized;
                var processedForce = forceDirection * _multiplier;
                rigidbody.AddForce(processedForce, ForceMode2D.Impulse);
            }

            return Task.CompletedTask;
        }
    }
}