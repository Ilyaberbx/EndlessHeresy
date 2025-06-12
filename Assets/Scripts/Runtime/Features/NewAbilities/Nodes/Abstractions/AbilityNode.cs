using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public abstract class AbilityNode
    {
        public abstract Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken);
    }
}