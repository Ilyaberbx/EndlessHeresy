using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Conditions.Runtime;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Actors.Hero.States;
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

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _statesAggregator = GetComponent<StatesAggregator<HeroActor>>();
            _healthComponent = GetComponent<HealthComponent>();
            _inputService = ServiceLocator.Get<InputService>();
            _statesAggregator.SetContext(this);
            var deadState = _statesAggregator.GetState<DeadState>();
            var transitions = _statesAggregator.Transitions;
            transitions.Any(deadState, new HealthStateCondition(_healthComponent, false));
            _statesAggregator.Start();
            await SetLocomotionState();
        }

        public Vector2 GetMovementInput() => _inputService.GetMovementInput();

        private async Task SetLocomotionState()
        {
            var setLocomotionStateTask = _statesAggregator.SetStateAsync<LocomotionState>(destroyCancellationToken);
            await setLocomotionStateTask;
        }
    }
}