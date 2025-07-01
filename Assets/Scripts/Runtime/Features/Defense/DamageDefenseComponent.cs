using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Affinity;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Defense
{
    public sealed class DamageDefenseComponent : MonoComponent
    {
        public event Action OnDefendedByImmune;
        public event Action OnAbsorbed;

        private const float DamageMultiplier = 2f;
        private AffinityComponent _affinity;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _affinity = Owner.GetComponent<AffinityComponent>();
            return Task.CompletedTask;
        }

        public float ApplyDefense(DamageType damageType, float damageValue, out bool isAbsorbed)
        {
            var defenseLevel = _affinity.GetDefenseLevel(damageType);
            isAbsorbed = false;

            switch (defenseLevel)
            {
                case DefenseLevelType.Neutral:
                    return damageValue;
                case DefenseLevelType.Weak:
                    return damageValue * DamageMultiplier;
                case DefenseLevelType.Strong:
                    return damageValue / DamageMultiplier;
                case DefenseLevelType.Immune:
                    OnDefendedByImmune?.Invoke();
                    return 0f;
                case DefenseLevelType.Absorb:
                    OnAbsorbed?.Invoke();
                    isAbsorbed = true;
                    return damageValue;
                default:
                    return damageValue;
            }
        }
    }
}