using System;
using System.Collections.Generic;
using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.Applicator.Installers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Inventory.Installers
{
    [Serializable]
    public sealed class EquipableItemInstaller : ItemComponentInstaller
    {
        [SerializeReference, Select] private ApplicatorInstaller[] _applicatorInstallers;
        [SerializeField] private List<AbilityMutationData> _abilityMutations;

        public override IItemComponent GetComponent()
        {
            var applicators = _applicatorInstallers.Select(temp => temp.GetApplicator()).ToArray();
            return new EquipableItemComponent(applicators);
        }
    }
}