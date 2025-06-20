using System.Threading;
using System.Threading.Tasks;
using Better.Conditions.Runtime;
using EndlessHeresy.Runtime.Conditions;

namespace EndlessHeresy.Runtime.Commands
{
    public abstract class ConditionalCommand : ICommand
    {
        private readonly Condition _chargingCondition;

        protected ConditionalCommand(Condition chargingCondition)
        {
            _chargingCondition = chargingCondition;
        }

        protected Condition GetCondition(IActor actor)
        {
            if (_chargingCondition is IActorCondition actorCondition)
            {
                actorCondition.Initialize(actor);
            }

            _chargingCondition.Validate();
            return _chargingCondition;
        }

        public abstract Task ExecuteAsync(IActor actor, CancellationToken cancellationToken);
    }
}