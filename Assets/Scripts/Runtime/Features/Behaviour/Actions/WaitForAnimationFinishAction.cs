using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Wait For Animation Finish", story: "Wait for [Animation] finished on [Actor]",
        category: "Action/EndlessHeresy", id: "5cdc95a6dea20b69416e29a933f96651")]
    public partial class WaitForAnimationFinishAction : Action
    {
        [SerializeReference] public BlackboardVariable<string> Animation;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }
}