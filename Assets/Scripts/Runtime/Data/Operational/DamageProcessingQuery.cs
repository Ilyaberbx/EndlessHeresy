using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.Data.Operational
{
    public struct DamageProcessingQuery
    {
        public DamageProcessingQuery(DamageData baseDamage, IReadOnlyList<BonusDamageData> bonusDamageData,
            StatsComponent attackerStats)
        {
            BaseDamage = baseDamage;
            BonusDamageData = bonusDamageData;
            AttackerStats = attackerStats;
        }

        public DamageData BaseDamage { get; }
        public IReadOnlyList<BonusDamageData> BonusDamageData { get; }
        public StatsComponent AttackerStats { get; }
    }
}