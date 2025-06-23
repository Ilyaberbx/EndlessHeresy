using System;
using EndlessHeresy.Runtime.Data.Static.Commands;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Execute Command", story: "Execute [Command] on [Actor]", category: "Action/EndlessHeresy",
        id: "3f41bf28ad77761d9707da0190fed45b")]
    public partial class ExecuteCommandAction : Action
    {
        [SerializeReference] public BlackboardVariable<CommandAsset> Command;
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