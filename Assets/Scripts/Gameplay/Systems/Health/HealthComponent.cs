using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.Health
{
    public sealed class HealthComponent : PocoComponent
    {
        public event Action OnHealthDepleted;
        public event Action<float> OnTakeDamage;
        
        private IStatsReadonly _statsComponent;
        private ReactiveProperty<int> _healthStat;
        private ReactiveProperty<int> _maxHealthStat;

        public int CurrentHp
        {
            get => _healthStat.Value;
            private set => _healthStat.Value = value;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statsComponent = Owner.GetComponent<StatsComponent>();
            _healthStat = _statsComponent.GetOrAdd(StatType.Health);
            _maxHealthStat = _statsComponent.GetOrAdd(StatType.MaxHealth);
            return Task.CompletedTask;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead())
            {
                return;
            }

            CurrentHp -= damage;
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

            var newValue = CurrentHp + healAmount;

            if (newValue > _maxHealthStat.Value)
            {
                CurrentHp = _maxHealthStat.Value;
                return;
            }

            CurrentHp += healAmount;
        }

        public bool IsDead() => _healthStat.Value <= 0;
    }
}