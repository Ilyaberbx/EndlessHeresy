using EndlessHeresy.Runtime.Services.Tick;
using EndlessHeresy.Runtime.Stats;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class TemporaryStatusEffectComponent : IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private const float MaxProgress = 1;

        private IGameUpdateService _gameUpdateService;
        private IStatusEffectRoot _root;

        private readonly float _duration;
        private float _elapsedTime;

        public TemporaryStatusEffectComponent(float duration) => _duration = duration;

        [Inject]
        public void Construct(IGameUpdateService gameUpdateService)
        {
            _gameUpdateService = gameUpdateService;
        }

        public void Initialize(IStatusEffectRoot root)
        {
            _root = root;
        }

        public void Apply(StatsContainer stats)
        {
            _gameUpdateService.OnUpdate += OnUpdate;
        }

        public void Remove(StatsContainer stats)
        {
            _gameUpdateService.OnUpdate -= OnUpdate;
        }

        public float GetProgress()
        {
            return MaxProgress - _elapsedTime / _duration;
        }

        private void OnUpdate(float deltaTime)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime < _duration)
            {
                return;
            }

            if (_root.TryGet<IdentifiedStatusEffectComponent>(out var identifiedStatusEffect))
            {
                _root.Owner.GetComponent<StatusEffectsComponent>().Remove(identifiedStatusEffect.Identifier);
            }
        }

        public void Reset() => _elapsedTime = 0;
    }
}