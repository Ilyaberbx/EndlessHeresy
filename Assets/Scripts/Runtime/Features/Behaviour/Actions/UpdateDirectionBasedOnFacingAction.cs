using System;
using EndlessHeresy.Runtime.Facing;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Update Direction Based On Facing", story: "Update [Direction] based on facing on [Actor]",
        category: "Action/EndlessHeresy", id: "7715c83e1981f28aa5bb8097c0cb9cad")]
    public partial class UpdateDirectionBasedOnFacingAction : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> Direction;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        protected override Status OnStart()
        {
            var actor = Actor.Value;

            if (!actor.TryGetComponent<FacingComponent>(out var facingComponent))
            {
                return Status.Failure;
            }


            var value = facingComponent.IsFacingRight ? Vector2.right : Vector2.left;

            Direction.Value = value.normalized;

            return Status.Running;
        }
    }
}