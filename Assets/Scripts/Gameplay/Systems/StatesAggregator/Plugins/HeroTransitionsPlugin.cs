using Better.Commons.Runtime.Extensions;
using Better.Conditions.Runtime;
using Better.StateMachine.Runtime.Modules.Transitions;
using EndlessHeresy.Core.States;
using EndlessHeresy.Core.StatesAggregator.Plugins;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Abilities.CrescentStrike;
using EndlessHeresy.Gameplay.Abilities.DoubleAttack;
using EndlessHeresy.Gameplay.Abilities.SingleAttack;
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