using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class ClassStatusEffectComponent : IStatusEffectComponent
    {
        public StatusEffectClassType Identifier { get; }

        public ClassStatusEffectComponent(StatusEffectClassType identifier) => Identifier = identifier;
    }
}