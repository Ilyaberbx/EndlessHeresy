using System;
using EndlessHeresy.Gameplay.Data.Identifiers;

namespace EndlessHeresy.Gameplay.Data.Persistant
{
    [Serializable]
    public sealed class StatData
    {
        public StatType Identifier;
        public int Value;
    }
}