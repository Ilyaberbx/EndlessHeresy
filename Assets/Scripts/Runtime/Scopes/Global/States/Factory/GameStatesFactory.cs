using VContainer;

namespace EndlessHeresy.Runtime.Scopes.Global.States.Factory
{
    public sealed class GameStatesFactory : IGameStatesFactory
    {
        private readonly IObjectResolver _container;

        public GameStatesFactory(IObjectResolver container) => _container = container;

        public BaseGameState Create<TState>() where TState : BaseGameState, new()
        {
            var state = new TState();
            _container.Inject(state);
            return state;
        }
    }
}