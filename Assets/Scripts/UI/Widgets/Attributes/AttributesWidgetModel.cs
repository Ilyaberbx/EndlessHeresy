using EndlessHeresy.Gameplay.Attributes;
using EndlessHeresy.UI.Core;

namespace EndlessHeresy.UI.Widgets.Attributes
{
    public sealed class AttributesWidgetModel : IModel
    {
        public IAttributesReadOnly Attributes { get; }

        public AttributesWidgetModel(IAttributesReadOnly attributes)
        {
            Attributes = attributes;
        }
    }
}