using System.Threading;
using System.Threading.Tasks;
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

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            Debug.Log(_message);
            return Task.CompletedTask;
        }
    }
}