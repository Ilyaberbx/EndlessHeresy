using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Persistant;
using EndlessHeresy.Runtime.Stats.Modifiers;
using UniRx;

namespace EndlessHeresy.Runtime.Attributes
{
    public sealed class Attribute
    {
        private readonly ReactiveProperty<int> _valueProperty;
        private readonly List<IStatModifierSource> _sources;
        public IReadOnlyReactiveProperty<int> ValueProperty => _valueProperty;
        public AttributeType Identifier { get; }

        public Attribute(int value, AttributeType identifier)
        {
            Identifier = identifier;
            _valueProperty = new ReactiveProperty<int>(value);
            _sources = new List<IStatModifierSource>();
        }

        public IStatModifierSource Increase()
        {
            _valueProperty.Value++;

            var source = new StatModifierSource();
            _sources.Add(source);

            return source;
        }

        public bool TryDecrease(out IStatModifierSource source)
        {
            if (_valueProperty.Value <= 0)
            {
                source = null;
                return false;
            }

            source = _sources[_valueProperty.Value];
            _valueProperty.Value--;
            return true;
        }

        public AttributeData GetSnapshot() => new()
        {
            Identifier = Identifier,
            Value = _valueProperty.Value
        };
    }
}