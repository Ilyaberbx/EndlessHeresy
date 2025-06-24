using System;
using EndlessHeresy.Runtime.Controls;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Behaviour.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Is Input Action Is Performed", story: "Is [InputAction] is performed on [Actor]",
        category: "Conditions/EndlessHeresy", id: "a75ef9eff8a04998ca2c7fab75610b00")]
    public partial class IsInputActionIsPerformedCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<InputActionReference> InputAction;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        public override bool IsTrue()
        {
            var actor = Actor.Value;

            if (!actor.TryGetComponent<InputActionPhaseRegistry>(out var registry))
            {
                return false;
            }

            if (!registry.TryGetPhase(InputAction.Value.action, out var phase))
            {
                return false;
            }

            return phase == InputActionPhase.Performed;
        }
    }
}