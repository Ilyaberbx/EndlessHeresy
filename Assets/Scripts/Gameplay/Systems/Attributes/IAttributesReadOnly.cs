using System.Collections.Generic;
using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Gameplay.Data.Persistant;

namespace EndlessHeresy.Gameplay.Attributes
{
    public interface IAttributesReadOnly
    {
        IReadOnlyList<ReactiveProperty<AttributeData>> GetAll();
    }
}