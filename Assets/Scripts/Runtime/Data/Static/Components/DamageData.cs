using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct DamageData
    {
        [SerializeField] private int _value;
        [SerializeField] private DamageType _identifier;

        public DamageType Identifier => _identifier;
        public int Value => _value;
    }
}