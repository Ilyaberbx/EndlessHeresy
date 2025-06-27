using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Huds.Cheats
{
    public sealed class CheatsHudViewModel : BaseViewModel<CheatsHudModel>
    {
        public IReactiveCollection<ItemType> AvailableItemsProperty { get; } = new ReactiveCollection<ItemType>();
        public IReactiveCollection<StatusEffectType> AvailableStatusEffectsProperty { get; } = new ReactiveCollection<StatusEffectType>();

        protected override void Initialize(CheatsHudModel model)
        {
            CollectAllItems();
            CollectAllStatusEffects();
        }

        public void AddItem(int index)
        {
            var itemToAdd = AvailableItemsProperty[index];

            if (itemToAdd == ItemType.None)
            {
                return;
            }

            Model.Inventory.Add(itemToAdd);
        }

        public void AddStatusEffect(int index)
        {
            var statusEffectToAdd = AvailableStatusEffectsProperty[index];

            if (statusEffectToAdd == StatusEffectType.None)
            {
                return;
            }

            Model.StatusEffects.Add(statusEffectToAdd);
        }

        private void CollectAllItems()
        {
            var enumValues = Enum.GetValues(typeof(ItemType));
            foreach (ItemType value in enumValues)
            {
                AvailableItemsProperty.Add(value);
            }
        }

        private void CollectAllStatusEffects()
        {
            var enumValues = Enum.GetValues(typeof(StatusEffectType));
            foreach (StatusEffectType value in enumValues)
            {
                AvailableStatusEffectsProperty.Add(value);
            }
        }
    }
}