using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Stats;
using UnityEngine;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class PeriodDamageStatusEffect : BaseStatusEffect, IUpdatableStatusEffect
    {
        private readonly PeriodDamageData _data;
        private float _elapsedTime;

        private HealthComponent _health;

        public PeriodDamageStatusEffect(PeriodDamageData data) => _data = data;

        public override void Apply(StatsComponent stats)
        {
            _elapsedTime = 0;
            var owner = stats.Owner;
            owner.TryGetComponent(out _health);
        }

        public override void Remove(StatsComponent stats) => _health = null;

        public void Update(IActor owner)
        {
            if (_health == null)
            {
                return;
            }

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