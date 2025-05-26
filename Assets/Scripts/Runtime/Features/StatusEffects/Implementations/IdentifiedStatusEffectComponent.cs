using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class IdentifiedStatusEffectComponent : IStatusEffectComponent
    {
        public StatusEffectType Identifier { get; }

        public IdentifiedStatusEffectComponent(StatusEffectType identifier)
        {
            Identifier = identifier;
        }
    }
}