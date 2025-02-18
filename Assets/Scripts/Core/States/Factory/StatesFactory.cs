using VContainer;

namespace EndlessHeresy.Core.States.Factory
{
    public sealed class StatesFactory<TDerivedState> : IStatesFactory<TDerivedState> where TDerivedState : new()
    {
        private readonly IObjectResolver _container;

        public StatesFactory(IObjectResolver container) => _container = container;

        public TState GetState<TState>() where TState : TDerivedState, new()
        {
            var state = new TState();
            _container.Inject(state);
            return state;
        }
    }
}