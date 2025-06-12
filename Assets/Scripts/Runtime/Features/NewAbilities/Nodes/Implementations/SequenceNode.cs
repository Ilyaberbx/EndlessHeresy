using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public sealed class SequenceNode : CollectionNode
    {
        public SequenceNode(AbilityNode[] children) : base(children)
        {
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