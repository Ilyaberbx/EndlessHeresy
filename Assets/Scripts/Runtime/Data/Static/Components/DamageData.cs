using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public sealed class DamageData
    {
        [SerializeField] private int _value;
        [SerializeField] private DamageType _identifier;

        public DamageType Identifier => _identifier;
        public int Value => _value;

        public DamageData(int value, DamageType identifier)
        {
            _value = value;
            _identifier = identifier;
        }
    }
}