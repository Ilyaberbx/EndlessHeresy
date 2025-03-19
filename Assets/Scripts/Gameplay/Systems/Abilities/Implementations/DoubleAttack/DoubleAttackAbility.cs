using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Gameplay.Abilities.DoubleAttack
{
    public sealed class DoubleAttackAbility : Ability
    {
        private int _firstHitDamage;
        private int _secondHitDamage;

        public void SetFirstHitDamage(int firstHitDamage) => _firstHitDamage = firstHitDamage;
        public void SetSecondHitDamage(int secondHitDamage) => _secondHitDamage = secondHitDamage;

        public override Task UseAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}