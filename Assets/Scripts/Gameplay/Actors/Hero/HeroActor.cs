using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Core.States;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Actors.Hero.States;
using EndlessHeresy.Gameplay.Conditions;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.States;

namespace EndlessHeresy.Gameplay.Actors.Hero
{
    public sealed class HeroActor : MonoActor, IContext
    {
        private StatesAggregator<HeroActor> _statesAggregator;
        private HealthComponent _healthComponent;

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            _statesAggregator = GetComponent<StatesAggregator<HeroActor>>();
            _healthComponent = GetComponent<HealthComponent>();
            _statesAggregator.SetContext(this);
            var deadState = _statesAggregator.GetState<DeadState>();
            var transitions = _statesAggregator.Transitions;
            transitions.Any(deadState, new HealthStateCondition(_healthComponent, false));
            await SetLocomotionState();
        }

        private async Task SetLocomotionState()
        {
            var setLocomotionStateTask = _statesAggregator.SetStateAsync<LocomotionState>(destroyCancellationToken);
            await setLocomotionStateTask;
        }
    }
}