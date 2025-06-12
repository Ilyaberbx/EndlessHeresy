using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public sealed class ParallelNode : AbilityNode
    {
        private readonly AbilityNode[] _children;

        public ParallelNode(AbilityNode[] children)
        {
            _children = children;
        }

        public override Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken)
        {
            var tasks = _children
                .Select(child => child.ExecuteAsync(context, cancellationToken))
                .ToList();

            return Task.WhenAll(tasks);
        }
    }
}