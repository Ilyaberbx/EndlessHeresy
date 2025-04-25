using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects
{
    public interface IStatusEffect
    {
        void Apply(StatsComponent stats);
        void Remove(StatsComponent stats);
        bool TryGet<TStatusEffect>(out TStatusEffect statusEffect) where TStatusEffect : class, IStatusEffect;
    }
}