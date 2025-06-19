using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Movement;

namespace EndlessHeresy.Runtime.Commands.Movement
{
    public sealed class ToggleMovementLock : ICommand
    {
        private readonly bool _isLocked;

        public ToggleMovementLock(bool isLocked)
        {
            _isLocked = isLocked;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            if (!actor.TryGetComponent<MovementComponent>(out var movementComponent))
            {
                return Task.CompletedTask;
            }

            if (_isLocked)
            {
                movementComponent.Lock();
            }
            else
            {
                movementComponent.Unlock();
            }

            return Task.CompletedTask;
        }
    }
}