using System;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Inventory.Installers
{
    [Serializable]
    public sealed class UsableItemInstaller : ItemComponentInstaller
    {
        [SerializeReference, Select] private CommandInstaller _commandInstaller;

        public override IItemComponent GetComponent()
        {
            return new UsableItemComponent(_commandInstaller.GetCommand());
        }
    }
}