using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Abilities.DoubleAttack;
using EndlessHeresy.Gameplay.Movement;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class DoubleAttackState : BaseState<HeroActor>
    {
        private AbilitiesCastComponent _abilitiesCastComponent;
        private MovementComponent _movementComponent;

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);

            _abilitiesCastComponent = context.GetComponent<AbilitiesCastComponent>();
            _movementComponent = context.GetComponent<MovementComponent>();
        }

        public override Task EnterAsync(CancellationToken token)
        {
            if (_abilitiesCastComponent.TryCast<DoubleAttackAbility>())
            {
                _movementComponent.Lock();
            }

            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _movementComponent.Unlock();
            return Task.CompletedTask;
        }
    }
}