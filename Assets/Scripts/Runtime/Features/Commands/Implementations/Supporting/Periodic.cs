using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EndlessHeresy.Runtime.Commands.Supporting
{
    public sealed class Periodic : ICommand
    {
        private readonly float _perSeconds;
        private readonly float _durationSeconds;
        private readonly ICommand _command;
        private float _periodElapsedTime;
        private float _totalElapsedTime;

        public Periodic(float perSeconds, float durationSeconds, ICommand command)
        {
            _perSeconds = perSeconds;
            _durationSeconds = durationSeconds;
            _command = command;
        }

        public async Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            _periodElapsedTime = 0;
            _totalElapsedTime = 0;

            do
            {
                await Task.Yield();
                _periodElapsedTime += Time.deltaTime;

                if (_periodElapsedTime < _perSeconds)
                {
                    return;
                }

                await _command.ExecuteAsync(actor, cancellationToken);
                _totalElapsedTime += _periodElapsedTime;
                _periodElapsedTime = 0;
            } while (!cancellationToken.IsCancellationRequested && _totalElapsedTime < _durationSeconds);
        }
    }
}