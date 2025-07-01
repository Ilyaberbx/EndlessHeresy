using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Defense;
using EndlessHeresy.Runtime.Evasion;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;
using EndlessHeresy.Runtime.Utilities;
using UniRx;

namespace EndlessHeresy.Runtime.Health
{
    public sealed class HealthComponent : PocoComponent
    {
        public event Action OnHealthDepleted;
        public event Action<float, DamageType, bool> OnTookDamage;

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

        public void TakeDamage(DamageData data, IActor attacker)
        {
            if (IsDead()) return;

            var damageToReceive = data.Value;

            if (_evasion.TryDodge())
            {
                return;
            }


            var isCritical = false;
            if (attacker != Owner)
            {
                damageToReceive = FightingUtility.ProcessDamage(attacker, damageToReceive, data.BonusMultiplier,
                    out isCritical);
            }

            damageToReceive = _damageDefense.ApplyDefense(data.Identifier, damageToReceive, out var isAbsorbed);

            if (damageToReceive <= 0)
            {
                return;
            }

            ApplyDamage(damageToReceive, data.Identifier, isAbsorbed, isCritical);
        }

        private void ApplyDamage(float damageToReceive, DamageType damageIdentifier, bool isAbsorbed, bool isCritical)
        {
            var modifier = new StatModifier(isAbsorbed ? damageToReceive : -damageToReceive, ModifierType.Flat);
            _healthStat.AddModifier(modifier);

            if (!isAbsorbed)
            {
                OnTookDamage?.Invoke(damageToReceive, damageIdentifier, isCritical);
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