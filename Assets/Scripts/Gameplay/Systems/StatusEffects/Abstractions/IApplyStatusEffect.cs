using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects
{
    public interface IApplyStatusEffect
    {
        void Apply(StatsContainer stats);
    }
}