using EndlessHeresy.Gameplay.Attributes;
using EndlessHeresy.UI.MVC;

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