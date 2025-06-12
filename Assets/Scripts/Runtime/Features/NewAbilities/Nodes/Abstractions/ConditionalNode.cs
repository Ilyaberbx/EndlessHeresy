using System.Threading;
using System.Threading.Tasks;
using Better.Conditions.Runtime;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public sealed class ConditionalNode : AbilityNode
    {
        private readonly AbilityNode _trueBranch;
        private readonly AbilityNode _falseBranch;
        private readonly Condition _condition;

        public ConditionalNode(AbilityNode trueBranch, AbilityNode falseBranch, Condition condition)
        {
            _trueBranch = trueBranch;
            _falseBranch = falseBranch;
            _condition = condition;
        }

        public override async Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken)
        {
            var condition = _condition.SafeInvoke();
            var selected = condition ? _trueBranch : _falseBranch;

            if (selected != null)
            {
                await selected.ExecuteAsync(context, cancellationToken);
            }
        }
    }
}