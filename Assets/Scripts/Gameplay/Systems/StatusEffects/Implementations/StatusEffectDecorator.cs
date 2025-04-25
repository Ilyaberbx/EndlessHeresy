using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public abstract class StatusEffectDecorator : BaseStatusEffect, IStatusEffect
    {
        protected IStatusEffect Core { get; }

        protected StatusEffectDecorator(IStatusEffect core)
        {
            Core = core;
        }

        public override void Apply(StatsComponent stats)
        {
            Core.Apply(stats);
        }

        public override void Remove(StatsComponent stats)
        {
            Core.Remove(stats);
        }

        public override bool TryGet<TStatusEffect>(out TStatusEffect statusEffect)
        {
            if (base.TryGet(out statusEffect))
            {
                return true;
            }

            switch (Core)
            {
                case TStatusEffect core:
                    statusEffect = core;
                    return true;
                case StatusEffectDecorator statusEffectDecorator:
                    return statusEffectDecorator.TryGet(out statusEffect);
                default:
                    return false;
            }
        }
    }
}