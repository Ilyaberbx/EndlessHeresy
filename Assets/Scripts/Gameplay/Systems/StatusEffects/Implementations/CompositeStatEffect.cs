using System.Collections.Generic;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class CompositeStatusEffect : IStatusEffect
    {
        private readonly IEnumerable<IStatusEffect> _effects;

        public CompositeStatusEffect(IEnumerable<IStatusEffect> effects)
        {
            _effects = new List<IStatusEffect>(effects);
        }

        public void Apply(StatsComponent stats)
        {
            foreach (var effect in _effects)
            {
                effect.Apply(stats);
            }
        }

        public void Remove(StatsComponent stats)
        {
            foreach (var effect in _effects)
            {
                effect.Remove(stats);
            }
        }
    }
}