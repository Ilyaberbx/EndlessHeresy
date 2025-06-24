using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;
using Unity.Behavior;
using UnityEngine;

namespace EndlessHeresy.Runtime.Behaviour.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "ListIsNullOrEmpty", story: "Is [GameObjectList] Null Or Empty",
        category: "Conditions/EndlessHeresy", id: "956be92647534a857e57e0b19dcd2145")]
    public partial class ListIsNullOrEmptyCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<List<GameObject>> GameObjectList;

        public override bool IsTrue()
        {
            return GameObjectList.Value.IsNullOrEmpty();
        }
    }
}