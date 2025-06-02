using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class PeriodDamageEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect
    {
        private readonly DamageData _data;
        private readonly Timer.Timer _timer;
        private HealthComponent _health;

        public PeriodDamageEffectComponent(DamageData data, Timer.Timer timer)
        {
            _data = data;
            _timer = timer;
        }

        public void Apply(StatsComponent stats)
        {
            _health = stats.Owner.GetComponent<HealthComponent>();
            _timer.OnTick += OnTick;
        }

        public void Remove(StatsComponent stats)
        {
            _timer.OnTick -= OnTick;
        }

        private void OnTick()
        {
            _health.TakeDamage(_data);
        }
    }
}