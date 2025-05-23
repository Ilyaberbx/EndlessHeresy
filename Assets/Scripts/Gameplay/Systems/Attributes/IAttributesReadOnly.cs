using EndlessHeresy.Gameplay.Data.Persistant;
using UniRx;

namespace EndlessHeresy.Gameplay.Attributes
{
    public interface IAttributesReadOnly
    {
        IReadOnlyReactiveCollection<AttributeModel> AttributesReadOnly { get; }
    }
}