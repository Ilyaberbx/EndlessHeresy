using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;

namespace EndlessHeresy.Gameplay.Inventory.Actions
{
    public sealed class RemoveItemAction : ItemAction
    {
        private readonly ItemType _itemType;
        public RemoveItemAction(ItemType itemType) => _itemType = itemType;

        protected override bool TryProcessInternally(IActor owner)
        {
            return Inventory.Remove(_itemType);
        }
    }
}