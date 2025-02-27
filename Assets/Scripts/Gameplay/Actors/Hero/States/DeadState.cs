using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Movement;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class DeadState : BaseState<HeroActor>
    {
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private Animator _animator;
        private MovementComponent _movementComponent;

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);
            _animator = context.GetComponent<AnimatorComponent>().Animator;
            _movementComponent = context.GetComponent<MovementComponent>();
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _movementComponent.Lock();
            _animator.SetBool(IsDead, true);
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;
    }
}