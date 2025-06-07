using EndlessHeresy.Runtime.Inventory;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory.Item
{
    public sealed class InventoryItemModel : IModel
    {
        private InventoryComponent _inventory;
        public ItemRoot Item { get; }
        public IActor Owner { get; }
        public InventoryComponent Inventory => _inventory ??= Owner.GetComponent<InventoryComponent>();

        public InventoryItemModel(ItemRoot item, IActor owner)
        {
            Item = item;
            Owner = owner;
        }
    }
}