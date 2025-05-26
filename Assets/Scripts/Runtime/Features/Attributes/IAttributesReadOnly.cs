using EndlessHeresy.Runtime.Data.Persistant;
using UniRx;

namespace EndlessHeresy.Runtime.Attributes
{
    public interface IAttributesReadOnly
    {
        IReadOnlyReactiveCollection<Attribute> AttributesReadOnly { get; }
        AttributeData[] GetSnapshot();
    }
}