using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;
using Attribute = EndlessHeresy.Runtime.Attributes.Attribute;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct AttributeItemData
    {
        [SerializeField] private AttributeType _identifier;
        [SerializeField] private StatModifierData[] _modifiers;
        [SerializeField] private Sprite _icon;

        public AttributeType Identifier => _identifier;
        public StatModifierData[] Modifiers => _modifiers;
        public Sprite Icon => _icon;

        public Attribute GetAttribute()
        {
            return new Attribute(0, _identifier);
        }
    }
}