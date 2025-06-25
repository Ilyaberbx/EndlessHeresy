using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Add Counter By Value", story: "Add [Counter] By [Value]", category: "Action/EndlessHeresy",
        id: "3103ab6e2c25b53c411e9757501d379c")]
    public partial class AddCounterByValueAction : Action
    {
        [SerializeReference] public BlackboardVariable<int> Value;
        [SerializeReference] public BlackboardVariable<int> Counter;

        protected override Status OnStart()
        {
            Counter.Value += Value.Value;
            return Status.Success;
        }
    }
}