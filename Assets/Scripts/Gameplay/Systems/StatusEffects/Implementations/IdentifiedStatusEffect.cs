using EndlessHeresy.Gameplay.Data.Identifiers;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class IdentifiedStatusEffect : StatusEffectDecorator
    {
        public StatusEffectType Identifier { get; }

        public IdentifiedStatusEffect(StatusEffectType identifier, IStatusEffect core) : base(core)
        {
            Identifier = identifier;
        }
    }
}