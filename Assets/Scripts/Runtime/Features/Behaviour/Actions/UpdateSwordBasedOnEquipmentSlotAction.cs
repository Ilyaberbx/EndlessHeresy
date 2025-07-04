using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.EquipmentSword;
using EndlessHeresy.Runtime.Generic;
using Unity.Behavior;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Update Sword based on Equipment slot",
        story: "Update Sword based on [EquipmentSlot] with on [Actor]",
        category: "Action/EndlessHeresy", id: "be9afa0d729039b23dffc021b06c5aea")]
    public partial class UpdateSwordBasedOnEquipmentSlotAction : Action
    {
        [SerializeReference] public BlackboardVariable<EquipmentSlotType> EquipmentSlot;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        protected override Status OnStart()
        {
            var actor = Actor.Value;
            var slotIdentifier = EquipmentSlot.Value;

            if (actor.IsUnityNull())
            {
                return Status.Failure;
            }

            if (!actor.TryGetComponent<EquipmentSwordStorage>(out var equipmentSwordStorage))
            {
                return Status.Failure;
            }

            if (!actor.TryGetComponent<LayerSpriteLibrary>(out var layerSpriteLibrary))
            {
                return Status.Failure;
            }

            if (!equipmentSwordStorage.TryGetSwordLibraryAsset(slotIdentifier, out var asset))
            {
                return Status.Failure;
            }

            layerSpriteLibrary.SetLibraryAsset(asset, AnimatorLayerType.Sword);

            return Status.Success;
        }
    }
}