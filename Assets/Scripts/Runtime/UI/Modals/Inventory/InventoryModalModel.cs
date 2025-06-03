using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Attributes;
using EndlessHeresy.Runtime.UI.Widgets.Inventory;

namespace EndlessHeresy.Runtime.UI.Modals.Inventory
{
    public sealed class InventoryModalModel : IModel
    {
        public AttributesModel AttributesModel { get; }
        public InventoryModel InventoryModel { get; }

        public InventoryModalModel(AttributesModel attributesModel, InventoryModel inventoryModel)
        {
            AttributesModel = attributesModel;
            InventoryModel = inventoryModel;
        }
    }
}