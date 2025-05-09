using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.Gameplay.Stats;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
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

            _health.TakeDamage(_data.Damage);
            _elapsedTime = 0;
        }
    }
}