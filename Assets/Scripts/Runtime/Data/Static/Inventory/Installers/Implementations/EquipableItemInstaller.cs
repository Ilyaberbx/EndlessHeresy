using System;
using System.Linq;
using EndlessHeresy.Runtime.Data.Static.Applicator.Installers;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Inventory.Installers
{
    [Serializable]
    public sealed class EquipableItemInstaller : ItemComponentInstaller
    {
        [SerializeField] private ApplicatorInstaller[] _applicatorInstallers;

        public override IItemComponent GetComponent()
        {
            return new EquipableItemComponent(_applicatorInstallers.Select(installer => installer.GetApplicator()));
        }
    }
}