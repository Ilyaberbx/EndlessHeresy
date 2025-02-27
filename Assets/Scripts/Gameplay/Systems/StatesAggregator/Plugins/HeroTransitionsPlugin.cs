using Better.Commons.Runtime.Extensions;
using Better.Conditions.Runtime;
using Better.StateMachine.Runtime.Modules.Transitions;
using EndlessHeresy.Core.States;
using EndlessHeresy.Core.StatesAggregator.Plugins;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Abilities;
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
            var abilitiesCastComponent = Context.GetComponent<AbilitiesCastComponent>();
            var abilitiesStorageComponent = Context.GetComponent<AbilitiesStorageComponent>();

            var isDeadCondition = new HealthStateCondition(healthComponent, false);
            var isAliveCondition = new HealthStateCondition(healthComponent, true);
            var anyToDashState = new AllComplexCondition(new Condition[]
            {
                isAliveCondition,
                new AbilityCastCondition<DashAbility>(abilitiesCastComponent, abilitiesStorageComponent)
            });
            var anyToLocomotion = new AllComplexCondition(new Condition[]
            {
                isAliveCondition,
                new ActiveAbilityCondition(abilitiesCastComponent, false)
            });

            var deadState = StatesFactory.CreateState<DeadState>();
            var locomotionState = StatesFactory.CreateState<LocomotionState>();
            var dashState = StatesFactory.CreateState<DashState>();

            deadState.SetContext(Context);
            locomotionState.SetContext(Context);
            dashState.SetContext(Context);

            transitionsModule.Any(locomotionState, anyToLocomotion);
            transitionsModule.Any(deadState, isDeadCondition);
            transitionsModule.Any(dashState, anyToDashState);

            StateMachine.Run();
            StateMachine.ChangeStateAsync(locomotionState).Forget();
        }
    }
}