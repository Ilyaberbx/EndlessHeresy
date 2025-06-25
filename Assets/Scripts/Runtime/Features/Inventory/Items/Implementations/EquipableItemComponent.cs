using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using EndlessHeresy.Runtime.Stats;
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
        }

        public IReactiveProperty<bool> IsEquipped { get; }

        public bool TryEquip(IActor actor, EquipmentSlotType slotIdentifier)
        {
            var inventory = actor.GetComponent<InventoryComponent>();
            var commandsInvoker = actor.GetComponent<CommandsInvokerComponent>();

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

            var command = _commandBySlot[slotIdentifier];
            commandsInvoker.Execute(command);
            IsEquipped.Value = true;
            return true;
        }

        public void Unequip(IActor actor, EquipmentSlotType slotIdentifier)
        {
            var commandsInvoker = actor.GetComponent<CommandsInvokerComponent>();

            if (IsEquipped.Value == false)
            {
                return;
            }

            var command = _commandBySlot[slotIdentifier];

            commandsInvoker.Execute(command.GetUndoCommand());

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