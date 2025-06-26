using System;
using EndlessHeresy.Runtime.Services.Tick;
using EndlessHeresy.Runtime.Stats;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class TemporaryEffectComponent : IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        public event Action<TemporaryEffectComponent> OnFinished;

        private const float MaxProgress = 1;
        private readonly IGameUpdateService _gameUpdateService;
        private readonly ReactiveProperty<float> _progress;
        private readonly float _duration;
        private float _elapsedTime;

        public IReadOnlyReactiveProperty<float> ProgressReadOnly => _progress;
        public StatusEffectRoot Root { get; private set; }

        public TemporaryEffectComponent(IGameUpdateService gameUpdateService, float duration)
        {
            _gameUpdateService = gameUpdateService;
            _duration = duration;
            _progress = new ReactiveProperty<float>();
        }

        public void Initialize(StatusEffectRoot root)
        {
            Root = root;
        }

        public void Apply(StatsComponent stats)
        {
            _gameUpdateService.OnUpdate += OnUpdate;
        }

        public void Remove(StatsComponent stats)
        {
            _gameUpdateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            _elapsedTime += Time.deltaTime;

            _progress.Value = MaxProgress - _elapsedTime / _duration;

            if (_elapsedTime < _duration)
            {
                return;
            }

            OnFinished?.Invoke(this);
            Root.Owner.GetComponent<StatusEffectsComponent>().Remove(Root.Identifier);
        }

        public void Reset() => _elapsedTime = 0;
    }
}