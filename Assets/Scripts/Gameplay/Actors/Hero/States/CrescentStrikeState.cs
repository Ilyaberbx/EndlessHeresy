using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Abilities.CrescentStrike;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public sealed class CrescentStrikeState : BaseState<HeroActor>
    {
        private AbilitiesCastComponent _abilitiesCastComponent;

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);

            _abilitiesCastComponent = context.GetComponent<AbilitiesCastComponent>();
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _abilitiesCastComponent.TryCast<CrescentStrikeAbility>();
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}