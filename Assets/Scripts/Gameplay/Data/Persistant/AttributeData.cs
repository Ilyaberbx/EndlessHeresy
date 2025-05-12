using System;
using EndlessHeresy.Gameplay.Data.Identifiers;

namespace EndlessHeresy.Gameplay.Data.Persistant
{
    [Serializable]
    public sealed class AttributeData
    {
        public AttributeType Identifier;
        public int Value;

        public AttributeData(AttributeType identifier, int value)
        {
            Identifier = identifier;
            Value = value;
        }
    }
}