using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Huds.Cheats
{
    public sealed class CheatsHudViewModel : BaseViewModel<CheatsHudModel>
    {
        public IReactiveCollection<ItemType> AvailableItemsProperty { get; } = new ReactiveCollection<ItemType>();

        protected override void Initialize(CheatsHudModel model)
        {
            CollectAllItems();
        }

        public void AddItem(int index)
        {
            var itemToAdd = AvailableItemsProperty[index];
            Model.Inventory.Add(itemToAdd);
        }

        private void CollectAllItems()
        {
            var enumValues = Enum.GetValues(typeof(ItemType));
            foreach (ItemType value in enumValues)
            {
                AvailableItemsProperty.Add(value);
            }
        }
    }
}