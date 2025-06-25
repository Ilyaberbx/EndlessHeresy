using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Clear Up Game Objects List", story: "Clear up [List]", category: "Action/EndlessHeresy",
        id: "47688f69d6979886cf73805532cf0d19")]
    public partial class ClearUpGameObjectsListAction : Action
    {
        [SerializeReference] public BlackboardVariable<List<GameObject>> List;

        protected override Status OnStart()
        {
            List.Value.Clear();
            return Status.Success;
        }
    }
}