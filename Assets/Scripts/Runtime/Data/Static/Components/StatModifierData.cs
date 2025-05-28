using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Stats.Modifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct StatModifierData
    {
        [SerializeField] private StatType _statIdentifier;
        [SerializeField] private ModifierType _identifier;
        [SerializeField] private float _value;

        public StatType StatIdentifier => _statIdentifier;

        public StatModifier GetStatModifier(IStatModifierSource source)
        {
            return new StatModifier(_value, _identifier, source);
        }
    }
}