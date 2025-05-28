using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public interface IRemoveStatusEffect
    {
        void Remove(StatsComponent stats);
    }
}