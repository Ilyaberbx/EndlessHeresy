using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Huds.Cheats
{
    public sealed class CheatsHudViewModel : BaseViewModel<CheatsHudModel>
    {
        public IReactiveCollection<ItemType> AvailableItemsProperty { get; } =
            new ReactiveCollection<ItemType>();

        public IReactiveCollection<StatusEffectType> AvailableStatusEffectsProperty { get; } =
            new ReactiveCollection<StatusEffectType>();

        public IReactiveCollection<DefenseLevelType> AffinityDefenseItemsProperty { get; } =
            new ReactiveCollection<DefenseLevelType>();

        public IReactiveCollection<DamageType> DamageTypesProperty { get; } =
            new ReactiveCollection<DamageType>();

        protected override void Initialize(CheatsHudModel model)
        {
            CollectAllItems();
            CollectAllStatusEffects();
            CollectAllAffinityDefenseItems();
            CollectAllDamageTypes();
        }

        public void AddInventoryItem(int index)
        {
            var itemToAdd = AvailableItemsProperty[index];

            if (itemToAdd == ItemType.None)
            {
                return;
            }

            Model.Inventory.Add(itemToAdd);
        }

        public void SetAffinityDefenseLevel(int affinityIndex, int damageTypeIndex)
        {
            var affinity = AffinityDefenseItemsProperty[affinityIndex];
            var damageType = DamageTypesProperty[damageTypeIndex];
            Model.Affinity.SetDefenseLevel(damageType, affinity);
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

        private void CollectAllDamageTypes()
        {
            var enumValues = Enum.GetValues(typeof(DamageType));
            foreach (DamageType value in enumValues)
            {
                DamageTypesProperty.Add(value);
            }
        }

        private void CollectAllAffinityDefenseItems()
        {
            var enumValues = Enum.GetValues(typeof(DefenseLevelType));
            foreach (DefenseLevelType value in enumValues)
            {
                AffinityDefenseItemsProperty.Add(value);
            }
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