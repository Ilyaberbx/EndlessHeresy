using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Stats.Modifiers
{
    public sealed class StatModifier
    {
        public float Value { get; }
        public ModifierType Identifier { get; }
        public IStatModifierSource Source { get; }
        public int Order { get; }

        public StatModifier(float value, ModifierType identifier, IStatModifierSource source = null)
        {
            Value = value;
            Identifier = identifier;
            Source = source;
            Order = (int)identifier;
        }
    }
}