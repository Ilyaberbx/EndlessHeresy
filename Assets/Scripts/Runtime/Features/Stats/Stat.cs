using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Stats.Modifiers;
using UniRx;

namespace EndlessHeresy.Runtime.Stats
{
    public sealed class Stat
    {
        private const int Accuracy = 4;
        private readonly IList<StatModifier> _modifiers;
        private readonly IReactiveProperty<float> _processedValueProperty;
        private readonly float _baseValue;

        public IReadOnlyReactiveProperty<float> ProcessedValueProperty => _processedValueProperty;
        public StatType Identifier { get; }

        public Stat(float baseValue, StatType identifier)
        {
            _baseValue = baseValue;
            Identifier = identifier;
            _processedValueProperty = new ReactiveProperty<float>(_baseValue);
            _modifiers = new List<StatModifier>();
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            _processedValueProperty.Value = GetProcessedValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (_modifiers.Remove(modifier))
            {
                _processedValueProperty.Value = GetProcessedValue();
            }
        }

        public bool RemoveAllModifiersBySource(IStatModifierSource source)
        {
            var modifiersToRemove = _modifiers.Where(temp => temp.Source == source).ToList();

            if (modifiersToRemove.Count == 0)
            {
                return false;
            }

            foreach (var modifier in modifiersToRemove)
            {
                _modifiers.Remove(modifier);
            }

            _processedValueProperty.Value = GetProcessedValue();
            return true;
        }

        private float GetProcessedValue()
        {
            var finalValue = _baseValue;

            foreach (var modifier in _modifiers)
            {
                switch (modifier.Identifier)
                {
                    case ModifierType.Flat:
                        finalValue += modifier.Value;
                        break;
                    case ModifierType.Percent:
                        finalValue *= 1 + modifier.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return (float)Math.Round(finalValue, Accuracy);
        }
    }
}