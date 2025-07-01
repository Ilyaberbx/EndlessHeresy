using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public sealed class DamageData
    {
        [SerializeField] private float _value;
        [SerializeField] private DamageType _identifier;
        [SerializeField] private float _bonusMultiplier;

        public DamageType Identifier => _identifier;
        public float Value => _value;
        public float BonusMultiplier => _bonusMultiplier;

        public DamageData(float value, DamageType identifier, float bonusMultiplier)
        {
            _value = value;
            _identifier = identifier;
            _bonusMultiplier = bonusMultiplier;
        }
    }
}