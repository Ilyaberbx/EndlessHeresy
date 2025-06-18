using System;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Inventory.Installers
{
    [Serializable]
    public sealed class StackableItemInstaller : ItemComponentInstaller
    {
        [SerializeField, Min(2)] private int _stackSize;

        public override IItemComponent GetComponent(IObjectResolver resolver)
        {
            return new StackableItemComponent(_stackSize);
        }
    }
}