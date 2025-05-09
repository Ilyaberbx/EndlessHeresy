using System;
using EndlessHeresy.Gameplay.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct DamageData
    {
        [SerializeField] private DamageType _identifier;
        [SerializeField] private int _value;

        public DamageType Identifier => _identifier;
        public int Value => _value;
    }
}