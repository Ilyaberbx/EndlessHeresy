using UnityEngine;

namespace EndlessHeresy.Runtime.Commands.Supporting
{
    public sealed class DebugCommand : ICommand
    {
        private readonly string _message;

        public DebugCommand(string message)
        {
            _message = message;
        }

        public void Execute()
        {
            Debug.Log(_message);
        }
    }
}