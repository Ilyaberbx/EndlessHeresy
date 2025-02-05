using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Animations;
using EndlessHeresy.Gameplay.Attack;
using EndlessHeresy.Gameplay.Movement;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class SingleAttackState : HeroState
    {
        private MeleeAttackComponent _meleeAttackComponent;
        private MovementComponent _movementComponent;
        private SingleAttackAnimation _singleAttackAnimation;

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);
            context.TryGetComponent(out _meleeAttackComponent);
            context.TryGetComponent(out _singleAttackAnimation);
            context.TryGetComponent(out _movementComponent);
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _movementComponent.Lock();
            _singleAttackAnimation.OnAttackTriggered += OnAttackTriggered;
            _singleAttackAnimation.Play();
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _singleAttackAnimation.OnAttackTriggered -= OnAttackTriggered;
            _movementComponent.Unlock();
            return Task.CompletedTask;
        }

        private void OnAttackTriggered()
        {
            _meleeAttackComponent.Attack();
        }
    }
}