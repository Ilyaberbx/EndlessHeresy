using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.UI;
using EndlessHeresy.Gameplay.StatusEffects;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.StatusEffects
{
    public abstract class StatusEffectConfiguration : ScriptableObject
    {
        [SerializeField] private StatusEffectType _identifier;
        [SerializeField] private int _duration;
        [SerializeField] private bool _isStackable;
        [SerializeField] private bool _resetOnStackAdded;
        [SerializeField] private StatusEffectUIData _uiData;

        public StatusEffectType Identifier => _identifier;
        public StatusEffectUIData UIData => _uiData;
        public bool IsTemporary => _duration > 0;
        public bool IsStackable => _isStackable;
        public int Duration => _duration;
        public abstract IStatusEffect GetStatusEffect();
    }
}