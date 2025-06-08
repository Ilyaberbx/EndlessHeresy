using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct AttributeModifierData
    {
        [SerializeField] private AttributeType _identifier;
        [SerializeField] private int _value;

        public AttributeType Identifier => _identifier;
        public int Value => _value;
    }
}