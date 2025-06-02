using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UniRx;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class StackableEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private readonly IList<IStatusEffectRoot> _activeEffects;
        private readonly IEnumerable<StatusEffectsBuilder> _effectsBuilders;
        private IStatusEffectRoot _root;
        public IReactiveProperty<int> StackCountProperty { get; }

        public StackableEffectComponent(List<StatusEffectsBuilder> effectsBuilders)
        {
            _effectsBuilders = effectsBuilders;
            _activeEffects = new List<IStatusEffectRoot>();
            StackCountProperty = new ReactiveProperty<int>(0);
        }

        public void Initialize(IStatusEffectRoot root) => _root = root;

        public void Apply(StatsComponent stats) => AddStack(stats);

        public void Remove(StatsComponent stats)
        {
            foreach (var effect in _activeEffects.OfType<IRemoveStatusEffect>())
            {
                effect.Remove(stats);
            }

            _activeEffects.Clear();
            StackCountProperty.Value = 0;
        }

        private void AddStack(StatsComponent stats)
        {
            var newStackIndex = _activeEffects.Count;

            if (newStackIndex >= _effectsBuilders.Count())
            {
                return;
            }

            var builder = _effectsBuilders.ElementAt(newStackIndex);
            var effect = builder.Build();
            effect.SetOwner(_root.Owner);

            if (effect is IApplyStatusEffect applyStatusEffect)
            {
                applyStatusEffect.Apply(stats);
            }

            _activeEffects.Add(effect);
            StackCountProperty.Value++;
        }
    }
}