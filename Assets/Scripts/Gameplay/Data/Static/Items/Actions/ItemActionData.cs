using System;
using EndlessHeresy.Gameplay.Inventory.Actions;

namespace EndlessHeresy.Gameplay.Data.Static.Items.Actions
{
    [Serializable]
    public abstract class ItemActionData
    {
        public abstract ItemAction GetAction();
    }
}