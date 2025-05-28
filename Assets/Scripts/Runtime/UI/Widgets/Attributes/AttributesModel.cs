using EndlessHeresy.Runtime.Attributes;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Widgets.Attributes
{
    public sealed class AttributesModel : IModel
    {
        public IReadOnlyReactiveCollection<Attribute> Attributes { get; }

        public AttributesModel(IReadOnlyReactiveCollection<Attribute> attributes)
        {
            Attributes = attributes;
        }
    }
}