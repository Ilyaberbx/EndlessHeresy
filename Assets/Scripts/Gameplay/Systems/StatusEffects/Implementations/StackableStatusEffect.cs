using System;
using System.Collections.Generic;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class StackableStatusEffect : IStatusEffect, IStackNotifier
    {
        public event Action<int> OnStackAdded;

        private readonly Func<int, IStatusEffect> _effectFactory;
        private readonly List<IStatusEffect> _activeStacks;
        public int StackCount => _activeStacks.Count;

        public StackableStatusEffect(Func<int, IStatusEffect> effectFactory)
        {
            _effectFactory = effectFactory;
            _activeStacks = new List<IStatusEffect>();
        }

        public void Apply(StatsComponent stats)
        {
            AddStack(stats);
        }

        public void Remove(StatsComponent stats)
        {
            foreach (var effect in _activeStacks)
            {
                effect.Remove(stats);
            }

            _activeStacks.Clear();
        }

        private void AddStack(StatsComponent stats)
        {
            var newStackIndex = _activeStacks.Count + 1;
            var effect = _effectFactory.Invoke(newStackIndex);
            effect.Apply(stats);
            _activeStacks.Add(effect);
            OnStackAdded?.Invoke(newStackIndex);
        }
    }
}