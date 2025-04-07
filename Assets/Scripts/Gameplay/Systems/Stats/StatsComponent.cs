using System.Collections.Generic;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Static.Components.Stats;

namespace EndlessHeresy.Gameplay.Stats
{
    public sealed class StatsComponent : PocoComponent, IStatsReadonly
    {
        private readonly Locator<BaseStat> _stats = new();

        public void SetStats(IReadOnlyList<BaseStatData> statsData)
        {
            foreach (var statData in statsData)
            {
                var identifier = statData.Identifier;
                _stats.Add(identifier, statData.GetStat());
            }
        }

        public bool TryGet<TStat>(out TStat stat) where TStat : BaseStat
        {
            return _stats.TryGet(typeof(TStat), out stat);
        }
    }
}