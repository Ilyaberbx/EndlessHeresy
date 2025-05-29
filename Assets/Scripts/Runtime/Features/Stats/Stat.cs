using System;
using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Stats.Modifiers;
using UniRx;

namespace EndlessHeresy.Runtime.Stats
{
    public sealed class Stat
    {
        private const int Accuracy = 4;

        private readonly List<StatModifier> _modifiers;
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
            _modifiers.Sort(CompareModifiersOrder);
            _processedValueProperty.Value = GetProcessedValue();
        }

        public bool RemoveAllModifiersBySource(IStatModifierSource source)
        {
            var didRemove = false;

            for (var i = 0; i < _modifiers.Count; i++)
            {
                var modifier = _modifiers[i];

                if (modifier.Source != source)
                {
                    continue;
                }

                _modifiers.RemoveAt(i);
                didRemove = true;
            }

            if (didRemove)
            {
                _processedValueProperty.Value = GetProcessedValue();
            }

            return didRemove;
        }

        private float GetProcessedValue()
        {
            var finalValue = _baseValue;
            var percentAdditiveSum = 0f;

            for (var i = 0; i < _modifiers.Count; i++)
            {
                var modifier = _modifiers[i];

                switch (modifier.Identifier)
                {
                    case ModifierType.Flat:
                        finalValue += modifier.Value;
                        break;

                    case ModifierType.PercentAdditive:
                        percentAdditiveSum += modifier.Value;
                        if (IsLastAdditive(i))
                        {
                            finalValue *= 1 + percentAdditiveSum;
                            percentAdditiveSum = 0f;
                        }

                        break;

                    case ModifierType.PercentMultiply:
                        finalValue *= 1 + modifier.Value;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(
                            nameof(modifier.Identifier),
                            modifier.Identifier,
                            "Unknown modifier type"
                        );
                }
            }

            return MathF.Round(finalValue, Accuracy);
        }

        private bool IsLastAdditive(int i)
        {
            return i + 1 >= _modifiers.Count || _modifiers[i + 1].Identifier != ModifierType.PercentAdditive;
        }

        private int CompareModifiersOrder(StatModifier x, StatModifier y)
        {
            if (x.Order < y.Order)
            {
                return -1;
            }

            return x.Order > y.Order ? 1 : 0;
        }
    }
}