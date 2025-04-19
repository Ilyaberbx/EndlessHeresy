using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public abstract class StatusEffectDecorator : IStatusEffect
    {
        protected IStatusEffect Core { get; }

        protected StatusEffectDecorator(IStatusEffect core)
        {
            Core = core;
        }

        public void Apply(StatsComponent stats)
        {
            Core.Apply(stats);
        }

        public void Remove(StatsComponent stats)
        {
            Core.Remove(stats);
        }
    }
}