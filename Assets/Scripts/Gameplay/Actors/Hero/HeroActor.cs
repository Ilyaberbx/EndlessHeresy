using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Actors.Hero.States;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Conditions;
using EndlessHeresy.Gameplay.Extensions;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Global.Services.Input;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Hero
{
    public sealed class HeroActor : MonoActor, IContext
    {
        private StatesAggregator<HeroActor> _statesAggregator;
        private HealthComponent _healthComponent;
        private InputService _inputService;
        public Vector2 GetMovementInput() => _inputService.GetMovementInput();

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            TryCollectRequiredComponents();
            _inputService = ServiceLocator.Get<InputService>();
            _statesAggregator.SetContext(this);
            var deadState = _statesAggregator.GetState<DeadState>();
            var transitions = _statesAggregator.Transitions;

            transitions.Any(deadState, new IsDeadCondition(_healthComponent));
            _statesAggregator.Start();
            await SetLocomotionState();
        }

        private void Update()
        {
            //TODO: Test

            if (Input.GetKeyDown(KeyCode.O))
            {
                _healthComponent.TakeDamage(1);
            }
        }

        private bool TryCollectRequiredComponents()
        {
            return TryGetComponent(out _statesAggregator) &&
                   TryGetComponent(out _healthComponent);
        }

        private async Task SetLocomotionState()
        {
            var setLocomotionStateTask = _statesAggregator.SetStateAsync<LocomotionState>(destroyCancellationToken);
            await setLocomotionStateTask;
        }
    }
}