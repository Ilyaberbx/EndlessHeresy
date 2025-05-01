using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class ComplexStatusEffect : BaseStatusEffect, IUpdatableStatusEffect
    {
        private readonly IStatusEffect[] _statusEffects;

        public ComplexStatusEffect(params IStatusEffect[] statusEffects)
        {
            _statusEffects = statusEffects;
        }

        public override void Apply(StatsComponent stats)
        {
            foreach (var statusEffect in _statusEffects)
            {
                statusEffect.Apply(stats);
            }
        }

        public override void Remove(StatsComponent stats)
        {
            foreach (var statusEffect in _statusEffects)
            {
                statusEffect.Remove(stats);
            }
        }

        public override bool TryGet<TStatusEffect>(out TStatusEffect statusEffect)
        {
            if (base.TryGet(out statusEffect))
            {
                return true;
            }

            foreach (var effect in _statusEffects)
            {
                if (effect.TryGet(out statusEffect))
                {
                    return true;
                }
            }

            return false;
        }

        public void Update(IActor owner)
        {
            foreach (var statusEffect in _statusEffects)
            {
                if (statusEffect is IUpdatableStatusEffect updatable)
                {
                    updatable.Update(owner);
                }
            }
        }
    }
}