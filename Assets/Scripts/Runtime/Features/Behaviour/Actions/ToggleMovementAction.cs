using System;
using EndlessHeresy.Runtime.Movement;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Toggle Movement ", story: "Toggle Movement for [Actor] [value]",
        category: "Action/EndlessHeresy", id: "2335113597e8ed6375c0070bcd0a0c7d")]
    public partial class ToggleMovementAction : Action
    {
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;
        [SerializeReference] public BlackboardVariable<bool> Value;

        private MovementComponent _movement;

        protected override Status OnStart()
        {
            if (Actor.Value == null)
            {
                return Status.Failure;
            }

            _movement = Actor.Value.GetComponent<MovementComponent>();

            if (_movement == null)
            {
                return Status.Failure;
            }

            if (Value.Value)
            {
                _movement.Unlock();
            }
            else
            {
                _movement.Lock();
            }

            return Status.Running;
        }
    }
}