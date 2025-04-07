using System;
using EndlessHeresy.Gameplay.Stats;
using EndlessHeresy.Gameplay.Stats.Implementations;

namespace EndlessHeresy.Gameplay.Data.Static.Components.Stats
{
    [Serializable]
    public sealed class LuckStatData : FloatStatData
    {
        public override BaseStat GetStat()
        {
            var stat = new LuckStat();
            stat.SetMin(MinValue);
            stat.SetMax(MaxValue);
            stat.SetValue(DefaultValue);
            return stat;
        }
    }
}