using System;
using System.Collections.Generic;
using EndlessHeresy.Gameplay.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct AttributeData
    {
        [SerializeField] private AttributeType _identifier;
        [SerializeField] private List<StatModifierData> _modifiers;
        [SerializeField] private int _minValue;
        [SerializeField] private string _displayName;
        [SerializeField] private Sprite _icon;

        public AttributeType Identifier => _identifier;
        public List<StatModifierData> Modifiers => _modifiers;
        public int MinValue => _minValue;
        public string DisplayName => _displayName;
        public Sprite Icon => _icon;
    }
}