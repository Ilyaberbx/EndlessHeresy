using System;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Inventory.Installers
{
    [Serializable]
    public abstract class ItemComponentInstaller
    {
        public abstract IItemComponent GetComponent(IObjectResolver resolver);
    }
}