using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Input
{
    public sealed class GameplayInputStorage : PocoComponent
    {
        public GameActions GameActions { get; private set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            GameActions = new GameActions();
            GameActions.Enable();
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            GameActions.Disable();
        }
    }
}