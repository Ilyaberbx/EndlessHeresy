using System;
using Unity.Behavior;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Assign Actor Position", story: "Assign [Actor] position to [Position]",
        category: "Action/EndlessHeresy", id: "b7aae1c8600761b7a65d74aaa7db7434")]
    public partial class AssignActorPositionAction : Action
    {
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;
        [SerializeReference] public BlackboardVariable<Vector2> Position;

        protected override Status OnStart()
        {
            var actor = Actor.Value;

            if (actor.IsUnityNull())
            {
                return Status.Failure;
            }

            Position.Value = actor.transform.position;

            return Status.Success;
        }
    }
}