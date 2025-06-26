using System.Collections.Generic;

namespace EndlessHeresy.Runtime.Commands
{
    public sealed class CommandsInvokerComponent : PocoComponent
    {
        private readonly Stack<ICommand> _commands = new();

        public void Execute(ICommand command)
        {
            if (command is IActorCommand actorCommand)
            {
                actorCommand.Setup(Owner);
            }

            _commands.Push(command);
            command.Execute();
        }

        protected override void OnDispose()
        {
            _commands.Clear();
        }
    }
}