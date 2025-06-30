using System.Collections.Generic;

namespace EndlessHeresy.Runtime.Commands
{
    public sealed class ComplexCommand : IActorCommand
    {
        private readonly IEnumerable<ICommand> _commands;
        private IActor _actor;

        public ComplexCommand(IEnumerable<ICommand> commands)
        {
            _commands = commands;
        }

        public void Setup(IActor actor)
        {
            _actor = actor;
        }

        public void Execute()
        {
            if (_actor == null)
            {
                return;
            }

            if (!_actor.TryGetComponent<CommandsInvokerComponent>(out var commandsInvoker))
            {
                return;
            }

            foreach (var command in _commands)
            {
                commandsInvoker.Execute(command);
            }
        }
    }
}