using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.ViewComponents;
using UnityEngine;

namespace EndlessHeresy.UI.Widgets.Attributes
{
    public sealed class AttributesWidgetView : BaseView
    {
        [SerializeField] private AttributeItemView[] _attributesView;

        public AttributeItemView[] AttributesView => _attributesView;
    }
}