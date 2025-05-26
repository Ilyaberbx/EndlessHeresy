using System;
using EndlessHeresy.Runtime.Inventory.Actions;

namespace EndlessHeresy.Runtime.Data.Static.Items.Actions
{
    [Serializable]
    public abstract class ItemActionData
    {
        public abstract ItemAction GetAction();
    }
}