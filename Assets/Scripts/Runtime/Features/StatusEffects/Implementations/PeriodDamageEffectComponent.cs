using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Services.Tick;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class PeriodDamageEffectComponent : PeriodicStatusEffectComponent
    {
        private readonly PeriodDamageData _data;
        private HealthComponent _health;

        public PeriodDamageEffectComponent(IGameUpdateService gameUpdateService, PeriodDamageData data) : base(
            gameUpdateService)
        {
            _data = data;
        }

        public override void Apply(StatsComponent stats)
        {
            base.Apply(stats);

            _health = stats.Owner.GetComponent<HealthComponent>();
        }

        protected override float GetInterval()
        {
            return _data.PerSeconds;
        }

        protected override void OnIntervalTick()
        {
            _health.TakeDamage(_data.DamageData);
        }
    }
}