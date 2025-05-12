using EndlessHeresy.UI.Core;
using EndlessHeresy.UI.Widgets.Attributes;
using UnityEngine;

namespace EndlessHeresy.UI.Modals.Inventory
{
    public sealed class InventoryModalView : BaseView
    {
        [SerializeField] private AttributesWidgetView _attributesWidgetView;

        public AttributesWidgetView AttributesWidgetView => _attributesWidgetView;
    }
}