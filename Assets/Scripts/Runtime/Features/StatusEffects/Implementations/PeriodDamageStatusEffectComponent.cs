using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Services.Tick;
using EndlessHeresy.Runtime.Stats;
using UnityEngine;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class PeriodDamageStatusEffectComponent : IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private readonly PeriodDamageData _data;
        private readonly IGameUpdateService _gameUpdateService;

        private IStatusEffectRoot _root;
        private HealthComponent _health;

        private float _elapsedTime;

        public PeriodDamageStatusEffectComponent(IGameUpdateService gameUpdateService, PeriodDamageData data)
        {
            _gameUpdateService = gameUpdateService;
            _data = data;
        }

        public void Initialize(IStatusEffectRoot root) => _root = root;

        public void Apply(StatsComponent stats)
        {
            _elapsedTime = 0;
            _root.Owner.TryGetComponent(out _health);
            _gameUpdateService.OnUpdate += OnUpdate;
        }

        public void Remove(StatsComponent stats)
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