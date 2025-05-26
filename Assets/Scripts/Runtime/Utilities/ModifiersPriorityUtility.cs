using EndlessHeresy.Runtime.Stats.Modifiers;

namespace EndlessHeresy.Runtime.Utilities
{
    public static class ModifiersPriorityUtility
    {
        public const int Addiction = 4;
        public const int Subtraction = 3;
        public const int Multiplying = 2;
        public const int Dividing = 1;

        public static int Comparison(IStatModifier a, IStatModifier b)
        {
            return a.GetModifierPriority().CompareTo(b.GetModifierPriority());
        }
    }
}