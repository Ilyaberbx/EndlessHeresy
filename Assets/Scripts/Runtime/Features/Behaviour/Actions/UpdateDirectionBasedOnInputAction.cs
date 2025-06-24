using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Update Direction Based On Input Action",
        story: "Update [Direction] based on [InputAction] with [normalized]", category: "Action/EndlessHeresy",
        id: "cf4905d8f5f6101ef4e416d0a997e215")]
    public partial class UpdateDirectionBasedOnInputAction : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> Direction;
        [SerializeReference] public BlackboardVariable<InputActionReference> InputAction;
        [SerializeReference] public BlackboardVariable<bool> Normalized;

        protected override Status OnStart()
        {
            var value = InputAction.Value.action.ReadValue<Vector2>();
            Direction.Value = Normalized.Value ? value.normalized : value;
            return Status.Running;
        }
    }
}