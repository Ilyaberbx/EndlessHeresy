using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public sealed class ParallelNode : CollectionNode
    {
        public ParallelNode(AbilityNode[] children) : base(children)
        {
        }

        public override Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken)
        {
            var tasks = Children
                .Select(child => child.ExecuteAsync(context, cancellationToken))
                .ToList();

            return Task.WhenAll(tasks);
        }
    }
}