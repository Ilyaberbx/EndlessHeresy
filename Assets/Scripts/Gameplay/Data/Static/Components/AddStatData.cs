using System;
using EndlessHeresy.Gameplay.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct AddStatData
    {
        [SerializeField] private int _value;
        [SerializeField] private StatType _identifier;

        public int Value => _value;
        public StatType Identifier => _identifier;
    }
}