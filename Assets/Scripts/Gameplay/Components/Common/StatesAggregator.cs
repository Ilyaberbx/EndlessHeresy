using System.Threading;
using System.Threading.Tasks;
using Better.StateMachine.Runtime;
using Better.StateMachine.Runtime.Modules;
using Better.StateMachine.Runtime.Modules.Transitions;
using EndlessHeresy.Commons;
using EndlessHeresy.Core;
using EndlessHeresy.Core.States;

namespace EndlessHeresy.Gameplay.Common
{
    public sealed class StatesAggregator<TContext> : PocoComponent where TContext : IContext
    {
        private IStateMachine<BaseState<TContext>> _stateMachine;
        private TContext _context;
        private bool IsInitialized => _stateMachine != null && _context != null;

        public AutoTransitionsModule<BaseState<TContext>> Transitions => _stateMachine.GetModule<BaseState<TContext>,
            AutoTransitionsModule<BaseState<TContext>>>();

        public void Setup(IStateMachine<BaseState<TContext>> stateMachine)
        {
            _stateMachine = stateMachine;
            _stateMachine.AddModule(new CacheModule<BaseState<TContext>>());
            _stateMachine.AddModule(new AutoTransitionsModule<BaseState<TContext>>());
            _stateMachine.AddModule(new LoggerModule<BaseState<TContext>>());
        }

        public void SetContext(TContext context) => _context = context;
        public void Start() => _stateMachine.Run();

        public Task SetStateAsync<TState>(CancellationToken token) where TState : BaseState<TContext>, new()
        {
            if (!IsInitialized || !_stateMachine.IsRunning)
            {
                return Task.CompletedTask;
            }

            var success = _stateMachine.TryGetModule(out CacheModule<BaseState<TContext>> module);

            if (!success)
            {
                return Task.CompletedTask;
            }

            var hasState = module.TryGetState(out TState state);

            if (!hasState)
            {
                state = GetState<TState>();
            }

            return _stateMachine.ChangeStateAsync(state, token);
        }

        public TState GetState<TState>() where TState : BaseState<TContext>, new()
        {
            if (!IsInitialized) return null;

            var success = _stateMachine.TryGetModule(out CacheModule<BaseState<TContext>> module);

            if (!success)
            {
                return new TState();
            }

            var state = module.GetOrAddState<TState>();
            state.SetContext(_context);
            return state;
        }
    }
}