using System.Collections.Generic;

namespace EndlessHeresy.Runtime.Commands
{
    public sealed class CommandsInvokerComponent : PocoComponent
    {
        private readonly Stack<ICommand> _commands = new();

        public void Execute(ICommand command)
        {
            _commands.Push(command);
            command.Execute();
        }
    }
}