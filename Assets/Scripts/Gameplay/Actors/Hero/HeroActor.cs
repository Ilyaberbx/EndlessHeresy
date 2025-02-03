using Better.Commons.Runtime.Extensions;
using Better.StateMachine.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Actors.Hero.States;

namespace EndlessHeresy.Gameplay.Actors.Hero
{
    public sealed class HeroActor : MonoActor
    {
        private IStateMachine<HeroState> _stateMachine;

        private void Start()
        {
            _stateMachine = new StateMachine<HeroState>();
            SetState<HeroIdleState>();
        }

        private void SetState<TState>() where TState : HeroState, new()
        {
            var state = new TState();
            _stateMachine.ChangeStateAsync(state, destroyCancellationToken).Forget();
        }
    }
}