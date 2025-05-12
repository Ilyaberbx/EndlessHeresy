using System;
using EndlessHeresy.Gameplay.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct AddAttributeData
    {
        [SerializeField] private AttributeType _identifier;
        [SerializeField] private int _value;

        public AttributeType Identifier => _identifier;
        public int Value => _value;
    }
}