using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Process Explosion Force",
        story: "Process Explosion with [ForceMultiplier] at [Point] for [Targets] with [Actor]",
        category: "Action/EndlessHeresy",
        id: "cc92b61ba3016cf1e7ebd9922ff2b868")]
    public partial class ProcessExplosionForceAction : Action
    {
        [SerializeReference] public BlackboardVariable<float> ForceMultiplier;
        [SerializeReference] public BlackboardVariable<Vector2> Point;
        [SerializeReference] public BlackboardVariable<List<GameObject>> Targets;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        protected override Status OnStart()
        {
            if (Targets.Value == null || Targets.Value.Count == 0)
            {
                return Status.Failure;
            }

            var selfActor = Actor.Value;
            var multiplier = ForceMultiplier.Value;
            var at = Point.Value;
            var targetActors = Targets.Value.Select(temp => temp.GetComponent<IActor>());
            var selfRigidbody = selfActor.GetComponent<RigidbodyStorageComponent>().Rigidbody;

            foreach (var targetActor in targetActors)
            {
                if (!targetActor.TryGetComponent<RigidbodyStorageComponent>(out var rigidbodyStorage))
                {
                    continue;
                }

                var rigidbody = rigidbodyStorage.Rigidbody;
                if (rigidbody == selfRigidbody)
                {
                    continue;
                }

                var actorPosition = rigidbodyStorage.Owner.Transform.position;
                var forceDirection = at.DirectionTo(actorPosition).normalized;
                var processedForce = forceDirection * multiplier;
                rigidbody.AddForce(processedForce, ForceMode2D.Impulse);
            }

            return Status.Success;
        }
    }
}