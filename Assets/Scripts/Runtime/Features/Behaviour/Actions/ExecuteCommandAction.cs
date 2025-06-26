using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Data.Static.Commands;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Execute Command", story: "Execute [Command] on [Actor]", category: "Action/EndlessHeresy",
        id: "2e858365c1dcbe35658e93120c86e58f")]
    public partial class ExecuteCommandAction : Action
    {
        [SerializeReference] public BlackboardVariable<CommandAsset> Command;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        protected override Status OnStart()
        {
            var actor = Actor.Value;

            if (actor == null)
            {
                return Status.Failure;
            }

            if (!actor.TryGetComponent<CommandsInvokerComponent>(out var commandsInvoker))
            {
                return Status.Failure;
            }

            var commandAsset = Command.Value;
            var command = commandAsset.GetCommand();
            commandsInvoker.Execute(command);
            return Status.Success;
        }
    }
}