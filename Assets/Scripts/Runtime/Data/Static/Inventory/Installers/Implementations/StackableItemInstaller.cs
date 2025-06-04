using System;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Inventory.Installers
{
    [Serializable]
    public sealed class StackableItemInstaller : ItemComponentInstaller
    {
        [SerializeField, Min(2)] private int _stackSize;

        public override IItemComponent GetComponent()
        {
            return new StackableItemComponent(_stackSize);
        }
    }
}