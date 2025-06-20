using System.Threading;
using System.Threading.Tasks;
using Better.Conditions.Runtime;

namespace EndlessHeresy.Runtime.Commands.Supporting
{
    public sealed class Fork : ConditionalCommand
    {
        private readonly ICommand _trueCommand;
        private readonly ICommand _falseCommand;

        public Fork(Condition chargingCondition, ICommand trueCommand, ICommand falseCommand) : base(chargingCondition)
        {
            _trueCommand = trueCommand;
            _falseCommand = falseCommand;
        }

        public override Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            return GetCondition(actor).SafeInvoke()
                ? _trueCommand.ExecuteAsync(actor, cancellationToken)
                : _falseCommand.ExecuteAsync(actor, cancellationToken);
        }
    }
}