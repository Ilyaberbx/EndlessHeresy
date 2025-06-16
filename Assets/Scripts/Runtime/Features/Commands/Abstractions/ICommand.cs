using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Commands
{
    public interface ICommand
    {
        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken);
    }
}