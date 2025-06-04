using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory
{
    public sealed class InventoryModel : IModel
    {
        public IReadOnlyReactiveCollection<ItemRoot> Items { get; }
        public int MaxSize { get; }
        public MonoActor Owner { get; }

        public InventoryModel(IReadOnlyReactiveCollection<ItemRoot> items, int maxSize, MonoActor owner)
        {
            Items = items;
            MaxSize = maxSize;
            Owner = owner;
        }
    }
}