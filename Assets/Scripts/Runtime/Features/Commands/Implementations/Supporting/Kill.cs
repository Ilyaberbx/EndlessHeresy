using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Commands.Supporting
{
    public sealed class Kill : ICommand
    {
        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            actor.GetComponent<CommandsComponent>().StopAll();
            return Task.CompletedTask;
        }
    }
}