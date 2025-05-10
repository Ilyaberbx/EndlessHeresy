using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Gameplay.Stats;
using VContainer;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class StackableStatusEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private readonly Func<int, IStatusEffectComponent> _effectFactory;
        private readonly List<IStatusEffectComponent> _activeStacks;
        private IStatusEffectRoot _root;
        private IObjectResolver _resolver;
        public int StackCount => _activeStacks.Count;

        public StackableStatusEffectComponent(Func<int, IStatusEffectComponent> effectFactory)
        {
            _effectFactory = effectFactory;
            _activeStacks = new List<IStatusEffectComponent>();
        }

        [Inject]
        public void Construct(IObjectResolver resolver) => _resolver = resolver;

        public void Initialize(IStatusEffectRoot root) => _root = root;
        public void Apply(StatsContainer stats) => AddStack(stats);

        public void Remove(StatsContainer stats)
        {
            foreach (var effect in _activeStacks.OfType<IRemoveStatusEffect>())
            {
                effect.Remove(stats);
            }

            _activeStacks.Clear();
        }

        private void AddStack(StatsContainer stats)
        {
            var newStackIndex = _activeStacks.Count + 1;
            var effect = _effectFactory.Invoke(newStackIndex);

            _resolver.Inject(effect);

            if (effect is IRootHandler rootHandler)
            {
                rootHandler.Initialize(_root);
            }

            if (effect is IApplyStatusEffect applyStatusEffect)
            {
                applyStatusEffect.Apply(stats);
            }

            _activeStacks.Add(effect);
        }
    }
}