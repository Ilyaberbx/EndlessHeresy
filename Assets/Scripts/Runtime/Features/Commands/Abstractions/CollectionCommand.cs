using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Commands
{
    public abstract class CollectionCommand : ICommand
    {
        protected ICommand[] Commands { get; }

        protected CollectionCommand(ICommand[] commands)
        {
            Commands = commands;
        }

        public abstract Task ExecuteAsync(IActor actor, CancellationToken cancellationToken);
    }
}