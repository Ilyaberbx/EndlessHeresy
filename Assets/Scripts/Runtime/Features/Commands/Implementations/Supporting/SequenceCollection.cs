using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Commands.Supporting
{
    public sealed class SequenceCollection : CollectionCommand
    {
        public SequenceCollection(ICommand[] commands) : base(commands)
        {
        }

        public override async Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            foreach (var command in Commands)
            {
                await command.ExecuteAsync(actor, cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}