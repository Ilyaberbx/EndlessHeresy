using System;
using System.Collections.Generic;
using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.Applicator.Installers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using UnityEngine;
using UnityEngine.Serialization;

namespace EndlessHeresy.Runtime.Data.Static.Inventory.Installers
{
    [Serializable]
    public sealed class EquipableItemInstaller : ItemComponentInstaller
    {
        [SerializeField] private EquipmentSlotApplicatorsData[] _slotsMutations;

        public override IItemComponent GetComponent()
        {
            var applicatorsBySlot = _slotsMutations
                .ToDictionary(temp => temp.Identifier,
                    temp => temp.ApplicatorInstallers.Select(installer => installer.GetApplicator())
                        .ToArray());
            return new EquipableItemComponent(applicatorsBySlot);
        }
    }
}