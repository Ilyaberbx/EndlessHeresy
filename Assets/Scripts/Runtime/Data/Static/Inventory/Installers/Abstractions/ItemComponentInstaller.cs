using System;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;

namespace EndlessHeresy.Runtime.Data.Static.Inventory.Installers
{
    [Serializable]
    public abstract class ItemComponentInstaller
    {
        public abstract IItemComponent GetComponent();
    }
}