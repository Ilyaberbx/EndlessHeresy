using System;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;

namespace EndlessHeresy.Gameplay.Utilities
{
    public static class StatusEffectsUtility
    {
        public static IStatusEffect Temporary(IStatusEffect effect, float duration)
        {
            return new TemporaryStatusEffect(effect, duration);
        }

        public static StackableStatusEffect Stackable(Func<int, IStatusEffect> effectFactory)
        {
            return new StackableStatusEffect(effectFactory);
        }
    }
}