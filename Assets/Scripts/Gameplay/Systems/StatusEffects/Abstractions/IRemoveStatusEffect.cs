using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects
{
    public interface IRemoveStatusEffect
    {
        void Remove(StatsComponent stats);
    }
}