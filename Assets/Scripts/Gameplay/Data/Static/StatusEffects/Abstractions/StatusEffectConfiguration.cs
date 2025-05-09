using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.UI;
using EndlessHeresy.Gameplay.StatusEffects.Builder;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.StatusEffects
{
    public abstract class StatusEffectConfiguration : ScriptableObject
    {
        [SerializeField] private StatusEffectClassType _classIdentifier;
        [SerializeField] private StatusEffectType _identifier;
        [SerializeField] private StatusEffectUIData _uiData;

        public StatusEffectType Identifier => _identifier;
        public StatusEffectUIData UIData => _uiData;

        public virtual void ConfigureBuilder(StatusEffectsBuilder builder)
        {
            builder.WithComponent(new IdentifiedStatusEffectComponent(Identifier));
            builder.WithComponent(new ClassStatusEffectComponent(_classIdentifier));
        }
    }
}