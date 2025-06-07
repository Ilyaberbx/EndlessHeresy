using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using EndlessHeresy.Runtime.Stats;
using UniRx;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class EquipableItemComponent : IItemComponent
    {
        private readonly IApplicator[] _applicators;
        public IReactiveProperty<bool> IsEquipped { get; }

        public EquipableItemComponent(IApplicator[] applicators)
        {
            _applicators = applicators;
            IsEquipped = new ReactiveProperty<bool>(false);
        }

        public bool TryEquip(IActor actor)
        {
            var inventory = actor.GetComponent<InventoryComponent>();

            var activeSlots = actor
                .GetComponent<StatsComponent>()
                .GetStat(StatType.ActiveEquipmentSlots)
                .ProcessedValueProperty
                .Value;

            var occupiedSlots = GetOccupiedSlots(inventory);

            if (occupiedSlots >= activeSlots)
            {
                return false;
            }

            foreach (var applicator in _applicators)
            {
                applicator.Apply(actor);
            }

            IsEquipped.Value = true;
            return true;
        }

        public void Unequip(IActor actor)
        {
            if (IsEquipped.Value == false)
            {
                return;
            }

            foreach (var applicator in _applicators)
            {
                applicator.Remove(actor);
            }

            IsEquipped.Value = false;
        }

        private static int GetOccupiedSlots(InventoryComponent inventory)
        {
            return inventory
                .Items
                .Count(temp =>
                    temp.Components.OfType<EquipableItemComponent>().Any(equipable => equipable.IsEquipped.Value));
        }
    }
}