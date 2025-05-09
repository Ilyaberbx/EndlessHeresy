using System.Linq;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class StatusEffectRoot : IStatusEffectRoot
    {
        private readonly IStatusEffectComponent[] _innerStatusEffects;

        public IActor Owner { get; private set; }

        public StatusEffectRoot(params IStatusEffectComponent[] innerStatusEffects)
        {
            _innerStatusEffects = innerStatusEffects;
        }

        public void Apply(StatsComponent stats)
        {
            foreach (var applyStatusEffect in _innerStatusEffects.OfType<IApplyStatusEffect>())
            {
                if (applyStatusEffect is IRootHandler rootHandler)
                {
                    rootHandler.Initialize(this);
                }

                applyStatusEffect.Apply(stats);
            }
        }

        public void Remove(StatsComponent stats)
        {
            foreach (var removeStatusEffect in _innerStatusEffects.OfType<IRemoveStatusEffect>())
            {
                removeStatusEffect.Remove(stats);
            }
        }

        public void SetOwner(IActor actor) => Owner = actor;

        public bool TryGet<TStatusEffect>(out TStatusEffect statusEffect)
            where TStatusEffect : class, IStatusEffectComponent
        {
            foreach (var innerStatusEffect in _innerStatusEffects)
            {
                if (innerStatusEffect is TStatusEffect concreteStatusEffect)
                {
                    statusEffect = concreteStatusEffect;
                    return true;
                }
            }

            statusEffect = null;
            return false;
        }

        public bool Has<TStatusEffect>() where TStatusEffect : class, IStatusEffectComponent
        {
            return _innerStatusEffects.OfType<TStatusEffect>().Any();
        }
    }
}