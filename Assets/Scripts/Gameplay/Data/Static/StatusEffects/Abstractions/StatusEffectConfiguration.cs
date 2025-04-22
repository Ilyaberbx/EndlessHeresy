using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.UI;
using EndlessHeresy.Gameplay.StatusEffects;
using UnityEngine;
using UnityEngine.Serialization;

namespace EndlessHeresy.Gameplay.Data.Static.StatusEffects
{
    public abstract class StatusEffectConfiguration : ScriptableObject
    {
        [SerializeField] private StatusEffectType _identifier;
        [SerializeField] private StatusEffectUIData _uiData;
        
        public StatusEffectType Identifier => _identifier;
        public StatusEffectUIData UIData => _uiData;
        public abstract IStatusEffect GetStatusEffect();
    }
}