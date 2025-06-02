using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class PeriodApplicatorEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private readonly IApplicator _applicator;
        private readonly Timer.Timer _timer;

        private IStatusEffectRoot _root;

        public PeriodApplicatorEffectComponent(IApplicator applicator, Timer.Timer timer)
        {
            _applicator = applicator;
            _timer = timer;
        }

        public void Initialize(IStatusEffectRoot root) => _root = root;
        public void Apply(StatsComponent stats) => _timer.OnTick += OnTick;
        public void Remove(StatsComponent stats) => _timer.OnTick -= OnTick;
        private void OnTick() => _applicator.Apply(_root.Owner);
    }
}