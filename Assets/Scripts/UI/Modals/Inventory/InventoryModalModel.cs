using EndlessHeresy.Gameplay.Attributes;
using EndlessHeresy.UI.Core;
using EndlessHeresy.UI.Widgets.Attributes;

namespace EndlessHeresy.UI.Modals.Inventory
{
    public sealed class InventoryModalModel : IModel
    {
        private readonly IAttributesReadOnly _attributesReadOnly;

        public InventoryModalModel(IAttributesReadOnly attributesReadOnly)
        {
            _attributesReadOnly = attributesReadOnly;
        }

        public AttributesWidgetModel GetAttributesWidgetModel() => new(_attributesReadOnly);
    }
}