using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Movement;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class DeadState : BaseHeroState
    {
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private MovementComponent _movementComponent;
        private AnimatorComponent _animatorComponent;
        private bool _isInitialized;

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);

            TryCollectRequiredComponents();
        }

        public override Task EnterAsync(CancellationToken token)
        {
            LockMovement();
            SetAnimatorDead();
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;

        private void SetAnimatorDead()
        {
            var animator = _animatorComponent.Animator;
            animator.SetTrigger(IsDead);
        }

        private void LockMovement() => _movementComponent.Lock();

        private bool TryCollectRequiredComponents() =>
            Context.TryGetComponent(out _movementComponent) &&
            Context.TryGetComponent(out _animatorComponent);
    }
}