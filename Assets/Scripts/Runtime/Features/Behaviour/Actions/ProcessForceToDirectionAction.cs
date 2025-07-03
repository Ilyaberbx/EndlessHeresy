using System;
using EndlessHeresy.Runtime.Generic;
using Unity.Behavior;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Process Force To Direction ",
        story: "Process Force To [Direction] with [Multiplier] for [Actor]", category: "Action/EndlessHeresy",
        id: "f4ddcb459c22fd2133b845f22dd409c8")]
    public partial class ProcessForceToDirectionAction : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> Direction;
        [SerializeReference] public BlackboardVariable<float> Multiplier;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        protected override Status OnStart()
        {
            var actor = Actor.Value;

            if (actor.IsUnityNull())
            {
                return Status.Failure;
            }

            if (!actor.TryGetComponent<RigidbodyStorageComponent>(out var rigidbodyStorageComponent))
            {
                return Status.Failure;
            }

            var forceDirection = Direction.Value;
            var force = forceDirection * Multiplier.Value;
            var rigidbody = rigidbodyStorageComponent.Rigidbody;
            rigidbody.AddForce(force, ForceMode2D.Impulse);
            return Status.Success;
        }
    }
}