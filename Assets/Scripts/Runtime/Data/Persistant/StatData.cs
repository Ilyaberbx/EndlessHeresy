using System;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Data.Persistant
{
    [Serializable]
    public sealed class StatData
    {
        public StatType Identifier;
        public int Value;
    }
}