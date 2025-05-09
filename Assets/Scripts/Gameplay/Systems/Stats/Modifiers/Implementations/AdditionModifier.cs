using EndlessHeresy.Gameplay.Utilities;

namespace EndlessHeresy.Gameplay.Stats.Modifiers
{
    public sealed class AdditionModifier : IStatModifier
    {
        private readonly int _value;

        public AdditionModifier(int value)
        {
            _value = value;
        }

        public int GetModifierPriority()
        {
            return ModifiersPriorityUtility.Addiction;
        }

        public int Modify(int value)
        {
            return value + _value;
        }

        public IStatModifier GetReversed()
        {
            return new SubtractionModifier(_value);
        }
    }
}