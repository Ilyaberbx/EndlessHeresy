using EndlessHeresy.Runtime.Services.Tick;
using EndlessHeresy.Runtime.Stats;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class TemporaryEffectComponent : IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private const float MaxProgress = 1;

        private readonly IGameUpdateService _gameUpdateService;
        private readonly ReactiveProperty<float> _progress;
        private readonly float _duration;
        private float _elapsedTime;
        private IStatusEffectRoot _root;

        public IReadOnlyReactiveProperty<float> ProgressReadOnly => _progress;

        public TemporaryEffectComponent(IGameUpdateService gameUpdateService, float duration)
        {
            _gameUpdateService = gameUpdateService;
            _duration = duration;
            _progress = new ReactiveProperty<float>();
        }

        public void Initialize(IStatusEffectRoot root)
        {
            _root = root;
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

            _root.Owner.GetComponent<StatusEffectsComponent>().Remove(_root.Identifier);
        }

        public void Reset() => _elapsedTime = 0;
    }
}