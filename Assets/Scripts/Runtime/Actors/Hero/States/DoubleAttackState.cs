using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.States;

namespace EndlessHeresy.Runtime.Actors.Hero.States
{
    public sealed class DoubleAttackState : BaseState<HeroActor>
    {
        private AbilitiesCastComponent _abilitiesCastComponent;

        protected override void OnContextSet(HeroActor context)
        {
            base.OnContextSet(context);
            _abilitiesCastComponent = context.GetComponent<AbilitiesCastComponent>();
        }

        public override Task EnterAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}