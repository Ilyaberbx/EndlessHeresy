using System.Collections.Generic;
using EndlessHeresy.Gameplay.Data.Persistant;

namespace EndlessHeresy.Gameplay.Attributes
{
    public interface IAttributesReadOnly
    {
        IReadOnlyList<AttributeData> GetAll();
    }
}