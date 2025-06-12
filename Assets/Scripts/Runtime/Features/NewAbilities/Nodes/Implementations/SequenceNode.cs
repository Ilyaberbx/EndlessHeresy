using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public sealed class SequenceNode : AbilityNode
    {
        public IList<AbilityNode> Children { get; }

        public SequenceNode(AbilityNode[] children)
        {
            Children = children;
        }

        public override async Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken)
        {
            foreach (var child in Children)
            {
                await child.ExecuteAsync(context, cancellationToken);
            }
        }
    }
}