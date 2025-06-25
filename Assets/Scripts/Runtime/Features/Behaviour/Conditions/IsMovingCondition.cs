using System;
using EndlessHeresy.Runtime.Generic;
using Unity.Behavior;
using UnityEngine;

namespace EndlessHeresy.Runtime.Behaviour.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Is Moving Condition", story: "Is [Actor] Moving with [Threshold]",
        category: "Conditions/EndlessHeresy", id: "b94b2da799250b4f359b00bf40a509f7")]
    public partial class IsMovingCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;
        [SerializeReference] public BlackboardVariable<float> Threshold;

        public override bool IsTrue()
        {
            var actor = Actor.Value;

            if (!actor.TryGetComponent<RigidbodyStorageComponent>(out var rigidbodyStorageComponent))
            {
                return false;
            }

            var rigidbody = rigidbodyStorageComponent.Rigidbody;
            return rigidbody.linearVelocity.sqrMagnitude > Threshold.Value;
        }
    }
}