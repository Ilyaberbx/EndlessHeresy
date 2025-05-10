using System.Collections.Generic;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.Components;

namespace EndlessHeresy.Gameplay.Stats
{
    public sealed class StatsContainer : PocoComponent
    {
        private readonly Locator<StatType, ReadOnlyReactiveProperty<int>> _stats = new();

        public void SetStats(IReadOnlyList<AddStatData> statsData)
        {
            foreach (var statData in statsData)
            {
                var property = new ReadOnlyReactiveProperty<int>(new ReactiveProperty<int>(statData.Value));
                _stats.Add(statData.Identifier, property);
            }
        }

        public ReadOnlyReactiveProperty<int> Get(StatType identifier)
        {
            if (_stats.TryGet(identifier, out var stat))
            {
                return stat;
            }

            _stats.Add(identifier, stat);
            return stat;
        }
    }
}