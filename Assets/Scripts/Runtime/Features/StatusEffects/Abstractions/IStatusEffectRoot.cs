using EndlessHeresy.Runtime.Actors;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public interface IStatusEffectRoot : IStatusEffectComponent, IApplyStatusEffect, IRemoveStatusEffect
    {
        bool TryGet<TStatusEffect>(out TStatusEffect statusEffect) where TStatusEffect : class, IStatusEffectComponent;
        bool Has<TStatusEffect>() where TStatusEffect : class, IStatusEffectComponent;
        public IActor Owner { get; }
    }
}