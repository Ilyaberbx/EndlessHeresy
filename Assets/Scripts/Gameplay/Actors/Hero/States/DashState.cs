using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Abilities;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class DashState : BaseState<HeroActor>
    {
        private AbilitiesCastComponent _abilitiesCast;

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);

            _abilitiesCast = context.GetComponent<AbilitiesCastComponent>();
        }

        public override Task EnterAsync(CancellationToken token)
        {
             _abilitiesCast.TryCast<DashAbility>();
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token) => Task.CompletedTask;
    }
}