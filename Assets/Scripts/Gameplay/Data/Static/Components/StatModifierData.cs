using System;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Stats.Modifiers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct StatModifierData
    {
        [SerializeField] private StatType _statIdentifier;
        [SerializeField] private ModifierType _identifier;
        [SerializeField] private int _value;

        public StatType StatIdentifier => _statIdentifier;

        public StatModifierData(StatType statIdentifier, ModifierType identifier, int value)
        {
            _statIdentifier = statIdentifier;
            _identifier = identifier;
            _value = value;
        }

        public IStatModifier GetInstance()
        {
            return _identifier switch
            {
                ModifierType.Multiplying => new MultiplyModifier(_value),
                ModifierType.Dividing => new DividingModifier(_value),
                ModifierType.Addition => new AdditionModifier(_value),
                ModifierType.Subtraction => new SubtractionModifier(_value),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}