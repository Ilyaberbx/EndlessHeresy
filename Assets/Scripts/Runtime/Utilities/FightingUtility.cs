using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Stats;
using UnityEngine;

namespace EndlessHeresy.Runtime.Utilities
{
    public static class FightingUtility
    {
        private const float MaxInclusive = 1f;

        public static float ProcessDamage(IActor attacker, float baseDamage, float multiplier, out bool isCritical)
        {
            isCritical = false;
            var stats = attacker.GetComponent<StatsComponent>();
            var criticalChance = stats.GetStat(StatType.CriticalChance).ProcessedValueProperty.Value;
            var criticalMultiplier = stats.GetStat(StatType.CriticalMultiplier).ProcessedValueProperty.Value;
            var power = stats.GetStat(StatType.Power).ProcessedValueProperty.Value;

            baseDamage += multiplier * power;

            if (Random.Range(0f, MaxInclusive) >= criticalChance)
            {
                return baseDamage;
            }

            baseDamage *= criticalMultiplier;
            isCritical = true;
            return baseDamage;
        }
    }
}