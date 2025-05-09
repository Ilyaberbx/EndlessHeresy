using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Utilities;

namespace EndlessHeresy.Gameplay.Stats.Modifiers
{
    public sealed class StatModifiersComponent : PocoComponent
    {
        private StatsComponent _statsComponent;

        private Dictionary<StatType, List<IStatModifier>> _modifiersMap;
        private Dictionary<StatType, ReactiveProperty<int>> _processedPropertiesMap;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statsComponent = Owner.GetComponent<StatsComponent>();
            _modifiersMap = new Dictionary<StatType, List<IStatModifier>>();
            _processedPropertiesMap = new Dictionary<StatType, ReactiveProperty<int>>();
            return Task.CompletedTask;
        }

        public void Process(StatModifierData data)
        {
            Process(data.StatIdentifier, data.GetInstance());
        }

        public void Reverse(StatModifierData data)
        {
            Process(data.StatIdentifier, data.GetInstance().GetReversed());
        }

        public ReadOnlyReactiveProperty<int> GetProcessedStat(StatType identifier)
        {
            if (!_processedPropertiesMap.ContainsKey(identifier))
            {
                _processedPropertiesMap.Add(identifier, _statsComponent.Get(identifier));
            }

            return new ReadOnlyReactiveProperty<int>(_processedPropertiesMap[identifier]);
        }

        private void Process(StatType identifier, IStatModifier modifier)
        {
            if (!_modifiersMap.TryGetValue(identifier, out var modifiers))
            {
                _modifiersMap.Add(identifier, new List<IStatModifier>()
                {
                    modifier
                });
                return;
            }

            modifiers.Add(modifier);
            modifiers.Sort(ModifiersPriorityUtility.Comparison);

            var value = _statsComponent.Get(identifier).Value;

            value = modifiers.Aggregate(value, (current, addedModifier) => addedModifier.Modify(current));

            if (_processedPropertiesMap.TryGetValue(identifier, out var processedProperty))
            {
                processedProperty.Value = value;
                return;
            }

            _processedPropertiesMap.Add(identifier, new ReactiveProperty<int>(value));
        }
    }
}