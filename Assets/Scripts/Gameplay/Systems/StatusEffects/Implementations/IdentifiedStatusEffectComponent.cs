using EndlessHeresy.Gameplay.Data.Identifiers;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
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