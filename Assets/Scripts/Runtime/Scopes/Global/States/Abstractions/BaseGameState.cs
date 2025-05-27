using System.Threading;
using System.Threading.Tasks;
using Better.StateMachine.Runtime.States;
using EndlessHeresy.Runtime.Services.Global.States;
using VContainer;

namespace EndlessHeresy.Runtime.Scopes.Global.States
{
    public abstract class BaseGameState : BaseState
    {
        protected IGameStatesService GameStatesService { get; private set; }

        [Inject]
        public void Construct(IGameStatesService gameStatesService) => GameStatesService = gameStatesService;

        public override Task EnterAsync(CancellationToken token) => Task.CompletedTask;

        public sealed override void OnEntered()
        {
        }

        public sealed override void OnExited()
        {
        }
    }
}