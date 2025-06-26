using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Data.Static.Commands;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Execute Command On Targets", story: "Execute [Command] On [Targets]",
        category: "Action/EndlessHeresy", id: "be724c4832300ff70c109f89a0feb484")]
    public partial class ExecuteCommandOnTargetsAction : Action
    {
        [SerializeReference] public BlackboardVariable<CommandAsset> Command;
        [SerializeReference] public BlackboardVariable<List<GameObject>> Targets;

        protected override Status OnStart()
        {
            var targetActors = Targets
                .Value
                .Where(temp => temp != null).Select(temp => temp.GetComponent<IActor>())
                .ToArray();

            foreach (var actor in targetActors)
            {
                if (!actor.TryGetComponent<CommandsInvokerComponent>(out var commandsInvoker))
                {
                    return Status.Failure;
                }

                var commandAsset = Command.Value;
                var command = commandAsset.GetCommand();
                commandsInvoker.Execute(command);
                return Status.Success;
            }

            return Status.Success;
        }
    }
}