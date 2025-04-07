using System;
using EndlessHeresy.Gameplay.Stats;
using EndlessHeresy.Gameplay.Stats.Implementations;

namespace EndlessHeresy.Gameplay.Data.Static.Components.Stats
{
    [Serializable]
    public sealed class HealthStatData : IntegerStatData
    {
        public override BaseStat GetStat()
        {
            var stat = new HealthStat();
            stat.SetMin(MinValue);
            stat.SetMax(MaxValue);
            stat.SetValue(DefaultValue);
            return stat;
        }
    }
}