using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.Health
{
    public sealed class HealthComponent : PocoComponent
    {
        public event Action OnHealthDepleted;
        public event Action<DamageData> OnTakeDamage;

        private Stat _healthStat;
        private StatsComponent _statsComponent;

        public float CurrentHealth => _healthStat.ProcessedValueProperty.Value;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statsComponent = Owner.GetComponent<StatsComponent>();
            _healthStat = _statsComponent.GetStat(StatType.CurrentHealth);
            return Task.CompletedTask;
        }

        public void TakeDamage(DamageData data)
        {
            if (IsDead())
            {
                return;
            }

            OnTakeDamage?.Invoke(data);

            if (IsDead())
            {
                OnHealthDepleted?.Invoke();
            }
        }

        public bool IsDead() => _healthStat.ProcessedValueProperty.Value <= 0;
    }
}