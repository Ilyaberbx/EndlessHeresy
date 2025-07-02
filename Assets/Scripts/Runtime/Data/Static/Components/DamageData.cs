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

        public DamageType Identifier => _identifier;
        public float Value => _value;

        public DamageData(float value, DamageType identifier)
        {
            _value = value;
            _identifier = identifier;
        }
    }
}