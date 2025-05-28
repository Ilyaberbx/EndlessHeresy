using System;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Data.Persistant
{
    [Serializable]
    public sealed class AttributeData
    {
        public AttributeType Identifier;
        public int Value;
    }
}