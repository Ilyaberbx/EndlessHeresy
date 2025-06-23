using EndlessHeresy.Runtime;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Play Animation ", story: "Play [Animation] on [Actor]", category: "Action/EndlessHeresy", id: "8c47f21655f04f2998ef09e81b78f67d")]
public partial class PlayAnimationAction : Action
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

