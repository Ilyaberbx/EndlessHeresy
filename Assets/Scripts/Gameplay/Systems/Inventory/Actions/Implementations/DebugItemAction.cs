using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Inventory.Actions
{
    public sealed class DebugItemAction : ItemAction
    {
        private readonly ItemType _itemType;
        private readonly string _name;

        public DebugItemAction(ItemType itemType, string name)
        {
            _itemType = itemType;
            _name = name;
        }

        protected override bool TryProcessInternally(IActor owner)
        {
            Debug.Log($"ItemAction: {_itemType} + {_name}");
            return true;
        }
    }
}