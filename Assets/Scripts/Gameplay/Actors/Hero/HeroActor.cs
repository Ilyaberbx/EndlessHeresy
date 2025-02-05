using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Conditions.Runtime;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Actors.Hero.States;
using EndlessHeresy.Gameplay.Animations;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Conditions;
using EndlessHeresy.Gameplay.Extensions;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Services.Input;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Hero
{
    public sealed class HeroActor : MonoActor, IContext
    {
        private IInputService _inputService;

        private StatesAggregator<HeroActor> _statesAggregator;
        private HealthComponent _healthComponent;
        private SingleAttackAnimation _singleAttackAnimation;

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            TryCollectRequiredComponents();
            _inputService = ServiceLocator.Get<InputService>();

            _statesAggregator.SetContext(this);
            var deadState = _statesAggregator.GetState<DeadState>();
            var locomotionState = _statesAggregator.GetState<LocomotionState>();
            var attackState = _statesAggregator.GetState<SingleAttackState>();
            var transitions = _statesAggregator.Transitions;
            var isAlive = new HealthStateCondition(_healthComponent, true);

            var attackConditions = new List<Condition>()
            {
                isAlive,
                new PredicateCondition(IsAttackPressed)
            };

            var attackToLocomotionConditions = new List<Condition>()
            {
                isAlive,
                new PredicateCondition(IsAttackFinished)
            };

            transitions.Any(deadState, new HealthStateCondition(_healthComponent, false));
            transitions.Any(attackState, new AllComplexCondition(attackConditions));
            transitions.AddTransition(attackState, locomotionState,
                new AllComplexCondition(attackToLocomotionConditions));

            _statesAggregator.Start();
            await SetLocomotionState();
        }

        public Vector2 GetMovementInput() => _inputService.GetMovementInput();
        private bool IsAttackFinished() => _singleAttackAnimation.IsAttackFinished;
        private bool IsAttackPressed() => _inputService.GetMouseButton(0);

        private bool TryCollectRequiredComponents() =>
            TryGetComponent(out _statesAggregator) &&
            TryGetComponent(out _healthComponent) &&
            TryGetComponent(out _singleAttackAnimation);

        private async Task SetLocomotionState()
        {
            var setLocomotionStateTask = _statesAggregator.SetStateAsync<LocomotionState>(destroyCancellationToken);
            await setLocomotionStateTask;
        }
    }
}