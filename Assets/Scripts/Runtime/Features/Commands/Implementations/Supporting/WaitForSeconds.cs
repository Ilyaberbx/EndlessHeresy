using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Commands.Supporting
{
    public sealed class WaitForSeconds : ICommand
    {
        private readonly float _seconds;

        public WaitForSeconds(float seconds)
        {
            _seconds = seconds;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            return Task.Delay((int)(_seconds * 1000), cancellationToken);
        }
    }
}