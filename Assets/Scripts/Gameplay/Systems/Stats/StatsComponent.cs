using System.Collections.Generic;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.Components;

namespace EndlessHeresy.Gameplay.Stats
{
    public sealed class StatsComponent : PocoComponent, IStatsReadonly
    {
        private readonly Locator<StatType, ReactiveProperty<int>> _stats = new();

        public void SetStats(IReadOnlyList<AddStatData> statsData)
        {
            foreach (var statData in statsData)
            {
                var property = new ReactiveProperty<int>(statData.Value);
                _stats.Add(statData.Identifier, property);
            }
        }

        public ReactiveProperty<int> Get(StatType identifier)
        {
            if (_stats.TryGet(identifier, out var stat))
            {
                return stat;
            }

            _stats.Add(identifier, stat);
            return stat;
        }

        public ReactiveProperty<int>[] GetAll() => _stats.GetElements();
    }
}