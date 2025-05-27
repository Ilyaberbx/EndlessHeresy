using Better.Commons.Runtime.Extensions;
using Better.Conditions.Runtime;
using Better.StateMachine.Runtime.Modules.Transitions;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Abilities.Conditions;
using EndlessHeresy.Runtime.Abilities.CrescentStrike;
using EndlessHeresy.Runtime.Abilities.DoubleAttack;
using EndlessHeresy.Runtime.Abilities.SingleAttack;
using EndlessHeresy.Runtime.Actors.Hero;
using EndlessHeresy.Runtime.Actors.Hero.States;
using EndlessHeresy.Runtime.Extensions;
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

            var anyToSingleAttack = new AllComplexCondition(new Condition[]
            {
                isAliveCondition,
                new AbilityCastCondition<SingleAttackAbility>(abilitiesCastComponent, abilitiesStorageComponent)
            });
            var anyToDoubleAttack = new AllComplexCondition(new Condition[]
            {
                isAliveCondition,
                new AbilityCastCondition<DoubleAttackAbility>(abilitiesCastComponent, abilitiesStorageComponent)
            });
            var anyToDash = new AllComplexCondition(new Condition[]
            {
                isAliveCondition,
                new AbilityCastCondition<DashAbility>(abilitiesCastComponent, abilitiesStorageComponent)
            });
            var anyToLocomotion = new AllComplexCondition(new Condition[]
            {
                isAliveCondition,
                new ActiveAbilityCondition(abilitiesCastComponent, false)
            });
            var anyToCrescentStrike = new AllComplexCondition(new Condition[]
            {
                isAliveCondition,
                new AbilityCastCondition<CrescentStrikeAbility>(abilitiesCastComponent, abilitiesStorageComponent)
            });

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

            transitionsModule.Any(locomotionState, anyToLocomotion);
            transitionsModule.Any(deadState, anyToDead);
            transitionsModule.Any(dashState, anyToDash);
            transitionsModule.Any(singleAttackState, anyToSingleAttack);
            transitionsModule.Any(doubleAttackState, anyToDoubleAttack);
            transitionsModule.Any(crescentStrikeState, anyToCrescentStrike);

            StateMachine.Run();
            StateMachine.ChangeStateAsync(locomotionState).Forget();
        }
    }
}