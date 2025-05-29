using System;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct PeriodStatModifierData
    {
        [SerializeField] private float _perSeconds;
        [SerializeField] private StatModifierData _modifierData;
        [SerializeField] private bool _isTemporary;

        public float PerSeconds => _perSeconds;
        public StatModifierData ModifierData => _modifierData;
        public bool IsTemporary => _isTemporary;
    }
}