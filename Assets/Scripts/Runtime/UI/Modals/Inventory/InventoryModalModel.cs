using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Attributes;

namespace EndlessHeresy.Runtime.UI.Modals.Inventory
{
    public sealed class InventoryModalModel : IModel
    {
        public AttributesModel AttributesModel { get; }

        public InventoryModalModel(AttributesModel attributesModel)
        {
            AttributesModel = attributesModel;
        }
    }
}