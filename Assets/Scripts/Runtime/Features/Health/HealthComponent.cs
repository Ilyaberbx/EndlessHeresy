using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Defense;
using EndlessHeresy.Runtime.Evasion;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;
using UniRx;

namespace EndlessHeresy.Runtime.Health
{
    public sealed class HealthComponent : PocoComponent
    {
        public event Action OnHealthDepleted;
        public event Action<DamageData, bool> OnTookDamage;

        private Stat _healthStat;
        private EvasionComponent _evasion;
        private DamageDefenseComponent _damageDefense;

        public IReadOnlyReactiveProperty<float> CurrentHealthProperty => _healthStat.ProcessedValueProperty;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _evasion = Owner.GetComponent<EvasionComponent>();
            _damageDefense = Owner.GetComponent<DamageDefenseComponent>();
            _healthStat = Owner.GetComponent<StatsComponent>().GetStat(StatType.CurrentHealth);

            _healthStat.ProcessedValueProperty.Where(value => value <= 0)
                .Take(1)
                .Subscribe(OnHealthReachedZero)
                .AddTo(CompositeDisposable);

            return Task.CompletedTask;
        }

        public void TakeDamage(DamageData data, bool isCritical)
        {
            if (IsDead()) return;

            var damageToReceive = data.Value;

            if (_evasion.TryDodge())
            {
                return;
            }

            damageToReceive = _damageDefense.ApplyDefense(data.Identifier, damageToReceive, out var isAbsorbed);

            if (damageToReceive <= 0)
            {
                return;
            }

            var processedData = new DamageData(damageToReceive, data.Identifier);
            ApplyDamage(processedData, isAbsorbed, isCritical);
        }

        private void ApplyDamage(DamageData data, bool isAbsorbed, bool isCritical)
        {
            var modifier = new StatModifier(isAbsorbed ? data.Value : -data.Value, ModifierType.Flat);
            _healthStat.AddModifier(modifier);

            if (!isAbsorbed)
            {
                OnTookDamage?.Invoke(data, isCritical);
            }
        }

        public bool IsDead() => _healthStat.ProcessedValueProperty.Value <= 0;

        private void OnHealthReachedZero(float value)
        {
            OnHealthDepleted?.Invoke();
        }

        public void Heal(float value)
        {
            if (IsDead())
            {
                return;
            }

            _healthStat.AddModifier(new StatModifier(value, ModifierType.Flat));
        }
    }
}