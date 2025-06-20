using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Commands.Supporting.For
{
    public sealed class ForLoop : ICommand
    {
        private readonly ICommand[] _commands;
        private readonly int _count;

        public ForLoop(ICommand[] commands, int count)
        {
            _commands = commands;
            _count = count;
        }

        public async Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            for (var i = 0; i < _count; i++)
            {
                foreach (var command in _commands)
                {
                    if (command is IIterationCommand iterationCommand)
                    {
                        iterationCommand.SetIndex(i);
                    }

                    await command.ExecuteAsync(actor, cancellationToken);
                }
            }
        }
    }
}