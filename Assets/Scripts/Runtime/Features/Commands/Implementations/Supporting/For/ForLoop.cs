using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Commands.Supporting.For
{
    public sealed class ForLoop : ICommand
    {
        private readonly IIterationCommand[] _iterationCommands;
        private readonly int _count;

        public ForLoop(IIterationCommand[] iterationCommands, int count)
        {
            _iterationCommands = iterationCommands;
            _count = count;
        }

        public async Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            for (var i = 0; i < _count; i++)
            {
                var command = _iterationCommands[i];
                command.SetIndex(i);
                await command.ExecuteAsync(actor, cancellationToken);
            }
        }
    }
}