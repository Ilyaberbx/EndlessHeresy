using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Stats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EndlessHeresy.Runtime.Evasion
{
    public sealed class EvasionComponent : PocoComponent
    {
        public event Action OnDodged;

        private Stat _evasionStat;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            var stats = Owner.GetComponent<StatsComponent>();
            _evasionStat = stats.GetStat(StatType.Evasion);
            return Task.CompletedTask;
        }

        public bool TryDodge()
        {
            var evasionValue = Mathf.Clamp(_evasionStat.ProcessedValueProperty.Value, 0f, 1f);
            var randomValue = Random.Range(0f, 1f);
            if (evasionValue >= randomValue)
            {
                OnDodged?.Invoke();
                return true;
            }

            return false;
        }
    }
}