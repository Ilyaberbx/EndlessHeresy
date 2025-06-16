using Better.Commons.Runtime.Extensions;
using Better.StateMachine.Runtime.Modules.Transitions;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Actors.Hero;
using EndlessHeresy.Runtime.Actors.Hero.States;
using EndlessHeresy.Runtime.Health;

namespace EndlessHeresy.Runtime.States.Aggregator.Plugins
{
    public sealed class HeroTransitionsPlugin : TransitionsPlugin<HeroActor>
    {
        protected override void ConfigureTransitions(TransitionsModule<BaseState<HeroActor>> transitionsModule)
        {
            var healthComponent = Context.GetComponent<HealthComponent>();
            var abilitiesCastComponent = Context.GetComponent<AbilitiesCastComponent>();
            var abilitiesStorageComponent = Context.GetComponent<AbilitiesStorageComponent>();

            var anyToDead = new HealthStateCondition(healthComponent, false);
            var isAliveCondition = new HealthStateCondition(healthComponent, true);

            var deadState = StatesFactory.CreateState<DeadState>();
            var locomotionState = StatesFactory.CreateState<LocomotionState>();
            var dashState = StatesFactory.CreateState<DashState>();
            var singleAttackState = StatesFactory.CreateState<SingleAttackState>();
            var doubleAttackState = StatesFactory.CreateState<DoubleAttackState>();
            var crescentStrikeState = StatesFactory.CreateState<CrescentStrikeState>();

            deadState.SetContext(Context);
            locomotionState.SetContext(Context);
            dashState.SetContext(Context);
            singleAttackState.SetContext(Context);
            doubleAttackState.SetContext(Context);
            crescentStrikeState.SetContext(Context);

            StateMachine.Run();
            StateMachine.ChangeStateAsync(locomotionState).Forget();
        }
    }
}