using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory.Item
{
    public sealed class InventoryItemModel : IModel
    {
        public ItemRoot Item { get; }
        public MonoActor Owner { get; }

        public InventoryItemModel(ItemRoot item, MonoActor owner)
        {
            Item = item;
            Owner = owner;
        }
    }
}