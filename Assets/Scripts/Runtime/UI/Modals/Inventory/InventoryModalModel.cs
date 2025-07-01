using EndlessHeresy.Runtime.Inventory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Equipment;
using EndlessHeresy.Runtime.UI.Widgets.Inventory;

namespace EndlessHeresy.Runtime.UI.Modals.Inventory
{
    public sealed class InventoryModalModel : IModel
    {
        public InventoryModel InventoryModel { get; }
        public EquipmentModel EquipmentModel { get; }

        public InventoryModalModel(InventoryComponent inventoryComponent)
        {
            var owner = inventoryComponent.Owner;
            InventoryModel = new InventoryModel(inventoryComponent.Items, inventoryComponent.MaxSize, owner);
            EquipmentModel = new EquipmentModel(inventoryComponent.Items, owner);
        }
    }
}