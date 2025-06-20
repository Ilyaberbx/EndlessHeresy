using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EndlessHeresy.Runtime.Commands.Supporting
{
    public sealed class Debug : ICommand
    {
        private readonly string _message;

        public Debug(string message)
        {
            _message = message;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            UnityEngine.Debug.Log(_message);
            return Task.CompletedTask;
        }
    }
}