using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;

namespace EndlessHeresy.Runtime.Applicators
{
    public sealed class StatModifiersApplicator : IApplicator
    {
        private readonly StatModifierData[] _modifiersData;
        private StatsComponent _stats;
        private Stat _stat;
        private StatModifierSource _source;

        public StatModifiersApplicator(StatModifierData[] modifiersData)
        {
            _modifiersData = modifiersData;
        }

        public void Apply(IActor actor)
        {
            _source = new StatModifierSource();

            foreach (var data in _modifiersData)
            {
                actor.GetComponent<StatsComponent>()
                    .GetStat(data.StatIdentifier)
                    .AddModifier(data.GetStatModifier(_source));
            }
        }

        public void Remove(IActor actor)
        {
            actor.GetComponent<StatsComponent>()
                .RemoveAllModifiersBySource(_source);
        }
    }
}