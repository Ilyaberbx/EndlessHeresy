using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Animations;
using EndlessHeresy.Gameplay.Attack;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class DoubleAttackState : HeroState
    {
        private MeleeAttackComponent _meleeAttackComponent;
        private DoubleAttackAnimation _doubleAttackAnimation;

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);

            context.TryGetComponent(out _meleeAttackComponent);
            context.TryGetComponent(out _doubleAttackAnimation);
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _doubleAttackAnimation.Play();
            _doubleAttackAnimation.OnFirstAttack += OnFirstAttackTriggered;
            _doubleAttackAnimation.OnSecondAttack += OnSecondAttackTriggered;
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _doubleAttackAnimation.OnFirstAttack -= OnFirstAttackTriggered;
            _doubleAttackAnimation.OnSecondAttack -= OnSecondAttackTriggered;
            return Task.CompletedTask;
        }

        private void OnSecondAttackTriggered()
        {
            _meleeAttackComponent.Attack();
        }

        private void OnFirstAttackTriggered()
        {
            _meleeAttackComponent.Attack();
        }
    }
}