using System.Threading;
using System.Threading.Tasks;
using Better.Conditions.Runtime;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public abstract class ConditionalNode : AbilityNode
    {
        private readonly AbilityNode _trueBranch;
        private readonly AbilityNode _falseBranch;

        protected ConditionalNode(AbilityNode trueBranch, AbilityNode falseBranch)
        {
            _trueBranch = trueBranch;
            _falseBranch = falseBranch;
        }

        public override async Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken)
        {
            var condition = GetCondition(context).SafeInvoke();
            var selected = condition ? _trueBranch : _falseBranch;

            if (selected != null)
            {
                await selected.ExecuteAsync(context, cancellationToken);
            }
        }

        protected abstract Condition GetCondition(AbilityContext context);
    }
}