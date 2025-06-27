using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;
using UniRx;

namespace EndlessHeresy.Runtime.Health
{
    public sealed class HealthComponent : PocoComponent
    {
        public event Action OnHealthDepleted;
        public event Action<DamageData> OnTookDamage;

        private Stat _healthStat;
        private StatsComponent _statsComponent;

        public IReadOnlyReactiveProperty<float> CurrentHealthProperty => _healthStat.ProcessedValueProperty;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statsComponent = Owner.GetComponent<StatsComponent>();
            _healthStat = _statsComponent.GetStat(StatType.CurrentHealth);
            _healthStat.ProcessedValueProperty.Where(value => value <= 0)
                .Take(1)
                .Subscribe(OnHealthReachedZero)
                .AddTo(CompositeDisposable);

            return Task.CompletedTask;
        }

        public void TakeDamage(DamageData data)
        {
            if (IsDead())
            {
                return;
            }

            var modifier = new StatModifier(-data.Value, ModifierType.Flat);
            _healthStat.AddModifier(modifier);
            OnTookDamage?.Invoke(data);
        }

        public bool IsDead() => _healthStat.ProcessedValueProperty.Value <= 0;

        private void OnHealthReachedZero(float value)
        {
            OnHealthDepleted?.Invoke();
        }
    }
}