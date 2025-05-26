using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Tick;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class PeriodDamageStatusEffectComponent : IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private readonly PeriodDamageData _data;

        private IGameUpdateService _gameUpdateService;
        private IStatusEffectRoot _root;
        private HealthComponent _health;

        private float _elapsedTime;
        public PeriodDamageStatusEffectComponent(PeriodDamageData data) => _data = data;

        [Inject]
        public void Construct(IGameUpdateService gameUpdateService) => _gameUpdateService = gameUpdateService;

        public void Initialize(IStatusEffectRoot root) => _root = root;

        public void Apply(StatsContainer stats)
        {
            _elapsedTime = 0;
            _root.Owner.TryGetComponent(out _health);
            _gameUpdateService.OnUpdate += OnUpdate;
        }

        public void Remove(StatsContainer stats)
        {
            _gameUpdateService.OnUpdate -= OnUpdate;
            _health = null;
        }

        private void OnUpdate(float deltaTime)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime < _data.PerSeconds)
            {
                return;
            }

            _health.TakeDamage(_data.DamageData);
            _elapsedTime = 0;
        }
    }
}