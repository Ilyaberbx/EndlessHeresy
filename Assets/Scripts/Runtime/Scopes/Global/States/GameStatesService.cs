using System;
using System.Threading;
using System.Threading.Tasks;
using Better.StateMachine.Runtime;
using EndlessHeresy.Runtime.Scopes.Global.States.Factory;
using EndlessHeresy.Runtime.States;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Scopes.Global.States
{
    [Serializable]
    public sealed class GameStatesService : IInitializable, IDisposable, IGameStatesService
    {
        private readonly IGameStatesFactory _gameStatesFactory;
        private IStateMachine<BaseGameState> _stateMachine;
        private CancellationTokenSource _tokenSource;

        public GameStatesService(IGameStatesFactory gameStatesFactory) => _gameStatesFactory = gameStatesFactory;

        public void Initialize()
        {
            _stateMachine = new StateMachine<BaseGameState>();
            _stateMachine.AddModule(new LoggerModule<BaseGameState>());
            _stateMachine.Run();
            _tokenSource = new CancellationTokenSource();
        }

        public void Dispose() => _tokenSource?.Dispose();

        public Task ChangeStateAsync<TState>() where TState : BaseGameState, new()
        {
            var state = _gameStatesFactory.Create<TState>();
            return _stateMachine.ChangeStateAsync(state, _tokenSource.Token);
        }
    }
}