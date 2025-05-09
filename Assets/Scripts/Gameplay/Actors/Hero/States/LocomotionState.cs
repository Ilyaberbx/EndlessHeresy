using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Movement;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class LocomotionState : BaseState<HeroActor>
    {
        private MovementComponent _movementComponent;

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);

            _movementComponent = context.GetComponent<MovementComponent>();
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _movementComponent.Unlock();
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _movementComponent.Lock();
            return Task.CompletedTask;
        }
    }
}