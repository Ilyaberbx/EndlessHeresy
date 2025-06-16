using System;
using System.Linq;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Inventory.Installers
{
    [Serializable]
    public sealed class EquipableItemInstaller : ItemComponentInstaller
    {
        [SerializeField] private EquipmentSlotCommandData[] _slotsMutations;

        public override IItemComponent GetComponent()
        {
            var applicatorsBySlot = _slotsMutations
                .ToDictionary(temp => temp.Identifier,
                    temp => temp.Installer.GetUndoableCommand());

            return new EquipableItemComponent(applicatorsBySlot);
        }
    }
}