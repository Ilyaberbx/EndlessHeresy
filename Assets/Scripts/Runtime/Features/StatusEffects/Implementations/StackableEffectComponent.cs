using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UniRx;
using VContainer;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class StackableEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private readonly IObjectResolver _resolver;
        private readonly int _maxStacks;
        private readonly StatusEffectBehaviourData[] _behavioursData;
        private readonly IList<StatusEffectRoot> _activeEffects;
        private StatusEffectRoot _root;
        public IReactiveProperty<int> StackCountProperty { get; }

        public StackableEffectComponent(IObjectResolver resolver,
            int maxStacks,
            StatusEffectBehaviourData[] behavioursData)
        {
            _resolver = resolver;
            _maxStacks = maxStacks;
            _behavioursData = behavioursData;
            _activeEffects = new List<StatusEffectRoot>();
            StackCountProperty = new ReactiveProperty<int>(0);
        }

        public void Initialize(StatusEffectRoot root) => _root = root;
        public void Apply(StatsComponent stats) => AddStack(stats);

        public void Remove(StatsComponent stats)
        {
            foreach (var effect in _activeEffects)
            {
                effect.Remove(stats);
            }

            _activeEffects.Clear();
            StackCountProperty.Value = 0;
        }

        private void AddStack(StatsComponent stats)
        {
            var newStack = _activeEffects.Count;
            if (newStack >= _maxStacks)
            {
                return;
            }

            var behavioursLength = _behavioursData.Length;
            var behaviourIndex = newStack % behavioursLength;
            var data = _behavioursData[behaviourIndex];
            var builder = new StatusEffectBuilder(_resolver);

            foreach (var installer in data.Installers)
            {
                installer.Install(builder);
            }

            var effect = builder.Build();
            effect.SetOwner(_root.Owner);
            effect.Apply(stats);
            _activeEffects.Add(effect);
            StackCountProperty.Value++;

            if (effect.TryGet<TemporaryEffectComponent>(out var temporaryEffect))
            {
                temporaryEffect.OnFinished += OnStackEffectFinished;
            }
        }

        private void OnStackEffectFinished(TemporaryEffectComponent temporaryEffect)
        {
            temporaryEffect.OnFinished -= OnStackEffectFinished;
            _activeEffects.Remove(temporaryEffect.Root);
            StackCountProperty.Value--;
        }
    }
}