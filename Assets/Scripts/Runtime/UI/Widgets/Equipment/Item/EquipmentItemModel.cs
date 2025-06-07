using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Widgets.Equipment.Item
{
    public sealed class EquipmentItemModel : IModel
    {
        public ItemRoot Item { get; }
        public IActor Owner { get; }

        public EquipmentItemModel(ItemRoot item, IActor owner)
        {
            Item = item;
            Owner = owner;
        }
    }
}