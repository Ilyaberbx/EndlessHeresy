using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public interface IStatusEffectRoot : IStatusEffectComponent, IApplyStatusEffect, IRemoveStatusEffect
    {
        StatusEffectType Identifier { get; }
        StatusEffectClassType ClassIdentifier { get; }
        IActor Owner { get; }
        bool TryGet<TStatusEffect>(out TStatusEffect statusEffect) where TStatusEffect : class, IStatusEffectComponent;
        bool Has<TStatusEffect>() where TStatusEffect : class, IStatusEffectComponent;
        void SetOwner(IActor actor);
    }
}