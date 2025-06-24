using System;
using EndlessHeresy.Runtime.Facing;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Toggle Facing", story: "Toggle Facing on [Actor] [Value]", category: "Action/EndlessHeresy",
        id: "9f172a24a41c0cc06aaa6d2a13494ac8")]
    public partial class ToggleFacingAction : Action
    {
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;
        [SerializeReference] public BlackboardVariable<bool> Value;

        protected override Status OnStart()
        {
            var actor = Actor.Value;
            if (!actor.TryGetComponent<FacingComponent>(out var facingComponent))
            {
                return Status.Failure;
            }

            if (!Value.Value)
            {
                facingComponent.Lock();
            }
            else
            {
                facingComponent.Unlock();
            }

            return Status.Running;
        }
    }
}