using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.UI;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using EndlessHeresy.Runtime.StatusEffects.Implementations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects
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