using Better.Commons.Runtime.Extensions;
using Better.StateMachine.Runtime.Modules.Transitions;
using EndlessHeresy.Core.States;
using EndlessHeresy.Core.StatesAggregator.Plugins;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Actors.Hero.States;
using EndlessHeresy.Gameplay.Conditions;
using EndlessHeresy.Gameplay.Health;

namespace EndlessHeresy.Gameplay.StatesAggregator.Plugins
{
    public sealed class HeroTransitionsPlugin : TransitionsPlugin<HeroActor>
    {
        protected override void ConfigureTransitions(TransitionsModule<BaseState<HeroActor>> transitionsModule)
        {
            var healthComponent = Context.GetComponent<HealthComponent>();

            var anyToDeadCondition = new HealthStateCondition(healthComponent, false);
            var anyToLocomotionCondition = new HealthStateCondition(healthComponent, true);

            var deadState = StatesFactory.CreateState<DeadState>();
            var locomotionState = StatesFactory.CreateState<LocomotionState>();

            deadState.SetContext(Context);
            locomotionState.SetContext(Context);

            transitionsModule.Any(locomotionState, anyToLocomotionCondition);
            transitionsModule.Any(deadState, anyToDeadCondition);

            StateMachine.Run();
            StateMachine.ChangeStateAsync(locomotionState).Forget();
        }
    }
}