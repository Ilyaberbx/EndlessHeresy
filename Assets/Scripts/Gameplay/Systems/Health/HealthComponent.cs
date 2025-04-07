using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Stats;
using EndlessHeresy.Gameplay.Stats.Implementations;

namespace EndlessHeresy.Gameplay.Health
{
    public sealed class HealthComponent : PocoComponent
    {
        private IStatsReadonly _statsComponent;
        private HealthStat _healthStat;
        public event Action OnHealthDepleted;
        public event Action<float> OnTakeDamage;
        public int CurrentHp => _healthStat.GetValue();

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statsComponent = Owner.GetComponent<StatsComponent>();
            _statsComponent.TryGet(out _healthStat);
            return base.OnPostInitializeAsync(cancellationToken);
        }

        public void TakeDamage(int damage)
        {
            if (IsDead())
            {
                return;
            }

            var currentValue = _healthStat.GetValue();
            _healthStat.SetValue(currentValue - damage);
            OnTakeDamage?.Invoke(damage);

            if (IsDead())
            {
                OnHealthDepleted?.Invoke();
            }
        }

        public void Heal(int healAmount)
        {
            if (IsDead())
            {
                return;
            }

            var currentValue = _healthStat.GetValue();
            _healthStat.SetValue(currentValue + healAmount);
        }

        public bool IsDead() => _healthStat.GetValue() <= 0;
    }
}