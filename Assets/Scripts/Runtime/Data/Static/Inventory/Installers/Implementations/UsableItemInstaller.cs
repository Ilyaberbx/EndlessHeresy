using System;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Inventory.Installers
{
    [Serializable]
    public sealed class UsableItemInstaller : ItemComponentInstaller
    {
        [SerializeReference, Select] private ICommandInstaller _commandInstaller;

        public override IItemComponent GetComponent(IObjectResolver resolver)
        {
            return new UsableItemComponent(_commandInstaller.GetCommand());
        }
    }
}