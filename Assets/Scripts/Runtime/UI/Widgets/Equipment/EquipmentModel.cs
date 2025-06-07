using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Widgets.Equipment
{
    public sealed class EquipmentModel : IModel
    {
        public IReadOnlyReactiveCollection<ItemRoot> Items { get; }
        public IActor Owner { get; }

        public EquipmentModel(IReadOnlyReactiveCollection<ItemRoot> items, IActor owner)
        {
            Items = items;
            Owner = owner;
        }
    }
}