using System;
using EndlessHeresy.Runtime.Movement;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Toggle Movement Animation Syncronizer", story: "Toggle Movement Animation Syncronizer on [Actor] with [Value]", category: "Action/EndlessHeresy", id: "07d0a4e46f73d700021cf92a6d1b35a7")]
    public partial class ToggleMovementAnimationSyncronizerAction : Action
    {
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;
        [SerializeReference] public BlackboardVariable<bool> Value;

        protected override Status OnStart()
        {
            if (Actor.Value == null)
            {
                return Status.Failure;
            }

            var syncronizer = Actor.Value.GetComponent<MovementAnimationSyncronizer>();

            if (syncronizer == null)
            {
                return Status.Failure;
            }

            if (Value.Value)
            {
                syncronizer.Unlock();
            }
            else
            {
                syncronizer.Lock();
            }

            return Status.Success;
        }
    }
}

