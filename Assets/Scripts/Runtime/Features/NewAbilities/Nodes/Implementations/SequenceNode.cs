using System.Threading;
using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public sealed class SequenceNode : AbilityNode
    {
        private readonly AbilityNode[] _children;

        public SequenceNode(AbilityNode[] children)
        {
            _children = children;
        }

        public override async Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken)
        {
            foreach (var child in _children)
            {
                await child.ExecuteAsync(context, cancellationToken);
            }
        }
    }
}