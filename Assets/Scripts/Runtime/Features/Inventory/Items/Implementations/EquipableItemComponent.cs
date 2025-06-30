using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using UniRx;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class EquipableItemComponent : IItemComponent
    {
        private readonly IDictionary<EquipmentSlotType, IUndoableCommand> _commandBySlot;

        public EquipableItemComponent(IDictionary<EquipmentSlotType, IUndoableCommand> commandBySlot)
        {
            _commandBySlot = commandBySlot;
            IsEquipped = new ReactiveProperty<bool>(false);
            OccupiedSlot = new ReactiveProperty<EquipmentSlotType>(EquipmentSlotType.None);
        }

        public IReactiveProperty<bool> IsEquipped { get; }
        public IReactiveProperty<EquipmentSlotType> OccupiedSlot { get; }

        public bool TryEquip(IActor actor, EquipmentSlotType slotIdentifier)
        {
            if (!actor.TryGetComponent<InventoryComponent>(out var inventory))
            {
                return false;
            }

            var items = inventory.Items;
            var isOccupied = IsOccupied(slotIdentifier, items);

            return true;
        }

        public void Unequip(IActor actor, EquipmentSlotType slotIdentifier)
        {
        }

        private static bool IsOccupied(EquipmentSlotType slotIdentifier, IReadOnlyReactiveCollection<ItemRoot> items)
        {
            return items
                .Count(root => root.Components.OfType<EquipableItemComponent>()
                    .Any(x => x.OccupiedSlot.Value == slotIdentifier)) == 0;
        }
    }
}