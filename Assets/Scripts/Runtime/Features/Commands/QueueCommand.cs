using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Commands
{
    public sealed class QueuedCommand
    {
        public ICommand Command { get; }
        public TaskCompletionSource<bool> Completion { get; }

        public QueuedCommand(ICommand command)
        {
            Command = command;
            Completion = new TaskCompletionSource<bool>();
        }
    }
}