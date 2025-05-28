using EndlessHeresy.Runtime.Attributes;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Widgets.Attributes.Item
{
    public sealed class AttributeItemModel : IModel
    {
        public Attribute Attribute { get; }

        public AttributeItemModel(Attribute attribute)
        {
            Attribute = attribute;
        }
    }
}