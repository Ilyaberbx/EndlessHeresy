using System.Threading;
using System.Threading.Tasks;
using Better.Conditions.Runtime;

namespace EndlessHeresy.Runtime.Commands.Supporting
{
    public sealed class Fork : ICommand
    {
        private readonly Condition _condition;
        private readonly ICommand _trueCommand;
        private readonly ICommand _falseCommand;

        public Fork(Condition condition, ICommand trueCommand, ICommand falseCommand)
        {
            _condition = condition;
            _trueCommand = trueCommand;
            _falseCommand = falseCommand;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            return _condition.SafeInvoke()
                ? _trueCommand.ExecuteAsync(actor, cancellationToken)
                : _falseCommand.ExecuteAsync(actor, cancellationToken);
        }
    }
}