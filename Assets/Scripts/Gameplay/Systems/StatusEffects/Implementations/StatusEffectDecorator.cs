using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public abstract class StatusEffectDecorator : BaseStatusEffect, IUpdatableStatusEffect
    {
        private IStatusEffect Core { get; }

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

        public override bool Has<TStatusEffect>()
        {
            return base.Has<TStatusEffect>() || Core.Has<TStatusEffect>();
        }

        public virtual void Update(IActor owner)
        {
            if (Core is IUpdatableStatusEffect updatable)
            {
                updatable.Update(owner);
            }
        }
    }
}