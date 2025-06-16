using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Commands.Supporting
{
    public sealed class ParallelCollection : CollectionCommand
    {
        public ParallelCollection(ICommand[] commands) : base(commands)
        {
        }

        public override Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var tasks = Commands.Select(temp => temp.ExecuteAsync(actor, cancellationToken));
            return Task.WhenAll(tasks);
        }
    }
}