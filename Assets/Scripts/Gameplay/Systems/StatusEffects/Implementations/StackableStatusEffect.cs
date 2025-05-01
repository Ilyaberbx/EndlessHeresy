using System;
using System.Collections.Generic;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class StackableStatusEffect : BaseStatusEffect, IStackNotifier, IUpdatableStatusEffect
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

        public override void Apply(StatsComponent stats)
        {
            AddStack(stats);
        }

        public override void Remove(StatsComponent stats)
        {
            _activeStacks.Clear();

            foreach (var effect in _activeStacks)
            {
                effect.Remove(stats);
            }
        }

        public void Update(IActor owner)
        {
            if (_activeStacks.Count == 0)
            {
                return;
            }

            foreach (var active in _activeStacks.ToArray())
            {
                if (active is IUpdatableStatusEffect updatable)
                {
                    updatable.Update(owner);
                }
            }
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