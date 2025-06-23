using EndlessHeresy.Runtime;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wait For Animation Event", story: "Wait for [AnimationEvent] on [Actor]", category: "Action/EndlessHeresy", id: "537b5f9df977bebdf6a177165e34b1c5")]
public partial class WaitForAnimationEventAction : Action
{
    [SerializeReference] public BlackboardVariable<string> AnimationEvent;
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

