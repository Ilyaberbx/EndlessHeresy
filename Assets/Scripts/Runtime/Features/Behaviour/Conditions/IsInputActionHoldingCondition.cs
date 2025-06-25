using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Behaviour.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Is InputAction Holding", story: "Is [InputAction] Holding equals [Value]",
        category: "Conditions/EndlessHeresy",
        id: "c014724f223084155eda56605626e592")]
    public partial class IsInputActionHoldingCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<InputActionReference> InputAction;
        [SerializeReference] public BlackboardVariable<bool> Value;

        public override bool IsTrue()
        {
            return InputAction.Value.action.ReadValue<float>() > 0f == Value.Value;
        }
    }
}