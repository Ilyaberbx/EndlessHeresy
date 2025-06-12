using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Applicators;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public sealed class ApplicatorsNode : AbilityNode
    {
        private readonly IApplicator[] _applicator;

        public ApplicatorsNode(IApplicator[] applicator)
        {
            _applicator = applicator;
        }

        public override Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken)
        {
            foreach (var applicator in _applicator)
            {
                applicator.Apply(context.Caster);
            }

            return Task.CompletedTask;
        }
    }
}