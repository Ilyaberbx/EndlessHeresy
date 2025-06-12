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
        private readonly IDictionary<EquipmentSlotType, IApplicator[]> _applicatorsBySlot;

        public EquipableItemComponent(IDictionary<EquipmentSlotType, IApplicator[]> applicatorsBySlot)
        {
            _applicatorsBySlot = applicatorsBySlot;
        }

        public IReactiveProperty<bool> IsEquipped { get; }

        public bool TryEquip(IActor actor, EquipmentSlotType slotIdentifier)
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

            var applicators = _applicatorsBySlot[slotIdentifier];

            foreach (var applicator in applicators)
            {
                applicator.Apply(actor);
            }

            IsEquipped.Value = true;
            return true;
        }

        public void Unequip(IActor actor, EquipmentSlotType slotIdentifier)
        {
            if (IsEquipped.Value == false)
            {
                return;
            }

            var applicators = _applicatorsBySlot[slotIdentifier];

            foreach (var applicator in applicators)
            {
                applicator.Apply(actor);
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