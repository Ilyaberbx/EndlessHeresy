using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Affinity
{
    public sealed class AffinityComponent : PocoComponent
    {
        private readonly Dictionary<DamageType, DefenseLevelType> _damageToDefenseMap = new();

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            foreach (var damageIdentifier in (DamageType[])Enum.GetValues(typeof(DamageType)))
            {
                _damageToDefenseMap.Add(damageIdentifier, DefenseLevelType.Neutral);
            }

            return Task.CompletedTask;
        }

        public DefenseLevelType GetDefenseLevel(DamageType damageType)
        {
            return _damageToDefenseMap[damageType];
        }

        public void SetDefenseLevel(DamageType damageType, DefenseLevelType defenseLevel)
        {
            _damageToDefenseMap[damageType] = defenseLevel;
            Debug.Log("UPD: " + defenseLevel + " for " + damageType);
        }
    }
}