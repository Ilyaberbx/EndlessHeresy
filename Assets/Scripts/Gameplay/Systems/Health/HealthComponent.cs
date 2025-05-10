using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Stats.Modifiers;

namespace EndlessHeresy.Gameplay.Health
{
    public sealed class HealthComponent : PocoComponent
    {
        public event Action OnHealthDepleted;
        public event Action<DamageData> OnTakeDamage;

        private ReadOnlyReactiveProperty<int> _healthStat;
        private StatModifiersComponent _statsModifiersComponent;

        public int CurrentHealth => _healthStat.Value;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statsModifiersComponent = Owner.GetComponent<StatModifiersComponent>();
            _healthStat = _statsModifiersComponent.GetProcessedStat(StatType.CurrentHealth);
            return Task.CompletedTask;
        }

        public void TakeDamage(DamageData data)
        {
            if (IsDead())
            {
                return;
            }

            var value = data.Value;
            var modifierData = new StatModifierData(StatType.CurrentHealth, ModifierType.Subtraction, value);
            _statsModifiersComponent.Process(modifierData);
            OnTakeDamage?.Invoke(data);

            if (IsDead())
            {
                OnHealthDepleted?.Invoke();
            }
        }

        public bool IsDead() => _healthStat.Value <= 0;
    }
}