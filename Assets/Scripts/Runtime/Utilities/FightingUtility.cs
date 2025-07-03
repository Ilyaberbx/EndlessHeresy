using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.Data.Static.Components;
using Random = UnityEngine.Random;

namespace EndlessHeresy.Runtime.Utilities
{
    public static class FightingUtility
    {
        private const float MaxInclusive = 1f;

        public static DamageData[] ProcessDamage(DamageProcessingQuery query, out bool isCritical)
        {
            isCritical = false;
            var processedDamage = new List<DamageData>()
            {
                query.BaseDamage
            };

            var attackerStats = query.AttackerStats;

            foreach (var bonusData in query.BonusDamageData)
            {
                var damageIdentifier = bonusData.DamageIdentifier;
                var statIdentifier = bonusData.StatIdentifier;
                var multiplier = bonusData.Multiplier;
                var statValue = attackerStats.GetStat(statIdentifier).ProcessedValueProperty.Value;
                var bonusDamageValue = statValue * multiplier;

                var existingDamage = processedDamage.FirstOrDefault(x => x.Identifier == damageIdentifier);
                if (existingDamage == null)
                {
                    processedDamage.Add(new DamageData(bonusDamageValue, damageIdentifier));
                    continue;
                }

                var index = processedDamage.IndexOf(existingDamage);
                processedDamage[index] = new DamageData(existingDamage.Value + bonusDamageValue, damageIdentifier);
            }

            var criticalChance = attackerStats.GetStat(StatType.CriticalChance).ProcessedValueProperty.Value;

            isCritical = Random.Range(0f, MaxInclusive) <= criticalChance;

            if (!isCritical)
            {
                return processedDamage.ToArray();
            }

            var criticalMultiplier = attackerStats.GetStat(StatType.CriticalMultiplier).ProcessedValueProperty.Value;
            var criticalDamageData = processedDamage.Select(CriticalDamageSelector(criticalMultiplier));
            return criticalDamageData.ToArray();
        }

        private static Func<DamageData, DamageData> CriticalDamageSelector(float criticalMultiplier)
        {
            return temp => new DamageData(temp.Value * criticalMultiplier, temp.Identifier);
        }
    }
}