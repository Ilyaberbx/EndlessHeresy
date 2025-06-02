using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;

namespace EndlessHeresy.Runtime.Applicators
{
    public sealed class StatModifiersApplicator : IApplicator, IStatModifierSource
    {
        private readonly StatModifierData[] _modifiersData;
        private StatsComponent _stats;
        private Stat _stat;

        public StatModifiersApplicator(StatModifierData[] modifiersData)
        {
            _modifiersData = modifiersData;
        }

        public void Apply(IActor actor)
        {
            foreach (var data in _modifiersData)
            {
                actor.GetComponent<StatsComponent>()
                    .GetStat(data.StatIdentifier)
                    .AddModifier(data.GetStatModifier(this));
            }
        }

        public void Remove(IActor actor)
        {
            actor.GetComponent<StatsComponent>()
                .RemoveAllModifiersBySource(this);
        }
    }
}