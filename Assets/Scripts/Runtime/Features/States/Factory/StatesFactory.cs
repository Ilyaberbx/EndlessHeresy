using VContainer;

namespace EndlessHeresy.Runtime.States.Factory
{
    public sealed class StatesFactory<TDerivedState> : IStatesFactory<TDerivedState>
    {
        private readonly IObjectResolver _container;

        public StatesFactory(IObjectResolver container) => _container = container;

        public TState CreateState<TState>() where TState : TDerivedState, new()
        {
            var state = new TState();
            _container.Inject(state);
            return state;
        }
    }
}