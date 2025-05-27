using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Persistant;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.Attributes
{
    public sealed class Attribute : IModel
    {
        private readonly AttributeData _data;
        public IReadOnlyReactiveProperty<int> ValueProperty { get; }
        public IReadOnlyReactiveProperty<Sprite> IconProperty { get; }
        public IReadOnlyReactiveProperty<AttributeType> IdentifierProperty { get; }
        public IReadOnlyReactiveProperty<string> DisplayNameProperty { get; }
        private IReadOnlyReactiveProperty<int> MinValueProperty { get; }
        private IReadOnlyReactiveCollection<StatModifierData> ModifiersProperty { get; set; }

        public Attribute(AttributeData data, AttributeItemData item)
        {
            _data = data;
            IdentifierProperty = item.Identifier;
            ValueProperty = new ReactiveProperty<int>(_data.Value);
            IconProperty = item.Icon;
            DisplayNameProperty = item.DisplayName;
            MinValueProperty = item.MinValue;
            ModifiersProperty = item.Modifiers;
        }

        public void Increase()
        {
            _data.Value++;
        }

        public bool TryDecrease()
        {
            if (_data.Value <= MinValueProperty.Value)
            {
                return false;
            }

            _data.Value--;
            return true;
        }

        public AttributeData GetSnapshot() => _data;
    }
}