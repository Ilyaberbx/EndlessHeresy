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
        story:
        "Process Explosion with [ForceMultiplier] at [Point] for [Targets] with [Actor] and assign to [ExplodedTargets]",
        category: "Action/EndlessHeresy",
        id: "cc92b61ba3016cf1e7ebd9922ff2b868")]
    public partial class ProcessExplosionForceAction : Action
    {
        [SerializeReference] public BlackboardVariable<float> ForceMultiplier;
        [SerializeReference] public BlackboardVariable<Vector2> Point;
        [SerializeReference] public BlackboardVariable<List<GameObject>> Targets;
        [SerializeReference] public BlackboardVariable<List<GameObject>> ExplodedTargets;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        protected override Status OnStart()
        {
            var selfActor = Actor.Value;
            var multiplier = ForceMultiplier.Value;
            var at = Point.Value;
            var targetActors = Targets.Value
                .Where(temp => temp != null)
                .Select(temp => temp.GetComponent<IActor>());
            var selfRigidbody = selfActor.GetComponent<RigidbodyStorageComponent>().Rigidbody;

            foreach (var targetActor in targetActors)
            {
                if (ExplodedTargets.Value.Contains(targetActor.GameObject))
                {
                    continue;
                }

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
                ExplodedTargets.Value.Add(targetActor.GameObject);
            }

            return Status.Success;
        }
    }
}