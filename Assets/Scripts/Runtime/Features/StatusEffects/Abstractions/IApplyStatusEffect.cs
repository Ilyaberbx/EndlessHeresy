using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public interface IApplyStatusEffect
    {
        void Apply(StatsContainer stats);
    }
}