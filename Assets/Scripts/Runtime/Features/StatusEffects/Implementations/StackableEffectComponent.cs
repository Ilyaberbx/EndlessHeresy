using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UniRx;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class StackableEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private readonly IList<StatusEffectRoot> _activeEffects;
        private readonly IEnumerable<StatusEffectsBuilder> _effectsBuilders;
        private StatusEffectRoot _root;
        public IReactiveProperty<int> StackCountProperty { get; }

        public StackableEffectComponent(List<StatusEffectsBuilder> effectsBuilders)
        {
            _effectsBuilders = effectsBuilders;
            _activeEffects = new List<StatusEffectRoot>();
            StackCountProperty = new ReactiveProperty<int>(0);
        }

        public void Initialize(StatusEffectRoot root) => _root = root;

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
            effect.Apply(stats);
            _activeEffects.Add(effect);
            StackCountProperty.Value++;
        }
    }
}