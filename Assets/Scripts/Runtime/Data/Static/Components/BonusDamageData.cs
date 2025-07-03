using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct BonusDamageData
    {
        [SerializeField] private float _multiplier;
        [SerializeField] private StatType _statIdentifier;
        [SerializeField] private DamageType _damageIdentifier;

        public float Multiplier => _multiplier;
        public StatType StatIdentifier => _statIdentifier;
        public DamageType DamageIdentifier => _damageIdentifier;
    }
}