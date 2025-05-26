using EndlessHeresy.Runtime.Utilities;
using UnityEngine;

namespace EndlessHeresy.Runtime.Stats.Modifiers
{
    public sealed class SubtractionModifier : IStatModifier
    {
        private readonly int _value;

        public SubtractionModifier(int value)
        {
            _value = value;
        }

        public int Modify(int value)
        {
            return Mathf.Clamp(value - _value, 0, value);
        }

        public IStatModifier GetReversed()
        {
            return new AdditionModifier(_value);
        }

        public int GetModifierPriority()
        {
            return ModifiersPriorityUtility.Subtraction;
        }
    }
}