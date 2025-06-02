using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class ApplicatorEffectComponent : IStatusEffectComponent, IApplyStatusEffect, IRemoveStatusEffect
    {
        private readonly IApplicator _applicator;

        public ApplicatorEffectComponent(IApplicator applicator)
        {
            _applicator = applicator;
        }

        public void Apply(StatsComponent stats)
        {
            _applicator.Apply(stats.Owner);
        }

        public void Remove(StatsComponent stats)
        {
            throw new System.NotImplementedException();
        }
    }
}