using EndlessHeresy.Gameplay.Data.Identifiers;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class ClassStatusEffectComponent : IStatusEffectComponent
    {
        public StatusEffectClassType Identifier { get; }

        public ClassStatusEffectComponent(StatusEffectClassType identifier) => Identifier = identifier;
    }
}