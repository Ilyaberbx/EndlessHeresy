using EndlessHeresy.Gameplay.Utilities;

namespace EndlessHeresy.Gameplay.Stats.Modifiers
{
    public sealed class MultiplyModifier : IStatModifier
    {
        private readonly int _value;

        public MultiplyModifier(int value)
        {
            _value = value;
        }

        public int GetModifierPriority()
        {
            return ModifiersPriorityUtility.Multiplying;
        }

        public int Modify(int value)
        {
            return value * _value;
        }

        public IStatModifier GetReversed()
        {
            return new DividingModifier(_value);
        }
    }
}