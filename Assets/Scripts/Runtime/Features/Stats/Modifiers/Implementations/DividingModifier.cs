using EndlessHeresy.Runtime.Utilities;

namespace EndlessHeresy.Runtime.Stats.Modifiers
{
    public sealed class DividingModifier : IStatModifier
    {
        private readonly int _value;

        public DividingModifier(int value)
        {
            _value = value;
        }

        public int GetModifierPriority()
        {
            return ModifiersPriorityUtility.Dividing;
        }

        public int Modify(int value)
        {
            return value / _value;
        }

        public IStatModifier GetReversed()
        {
            return new MultiplyModifier(_value);
        }
    }
}