using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct AttributeItemData
    {
        [SerializeField] private ReactiveProperty<AttributeType> _identifier;
        [SerializeField] private ReactiveCollection<StatModifierData> _modifiers;
        [SerializeField] private ReactiveProperty<int> _minValue;
        [SerializeField] private ReactiveProperty<string> _displayName;
        [SerializeField] private ReactiveProperty<Sprite> _icon;

        public ReactiveProperty<AttributeType> Identifier => _identifier;
        public ReactiveCollection<StatModifierData> Modifiers => _modifiers;
        public ReactiveProperty<int> MinValue => _minValue;
        public ReactiveProperty<string> DisplayName => _displayName;
        public ReactiveProperty<Sprite> Icon => _icon;
    }
}