using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Facing;

namespace EndlessHeresy.Runtime.Commands.Facing
{
    public sealed class ToggleFacingLock : ICommand
    {
        private readonly bool _isLocked;

        public ToggleFacingLock(bool isLocked)
        {
            _isLocked = isLocked;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            if (!actor.TryGetComponent<FacingComponent>(out var facingComponent))
            {
                return Task.CompletedTask;
            }

            if (_isLocked)
            {
                facingComponent.Lock();
            }
            else
            {
                facingComponent.Unlock();
            }

            return Task.CompletedTask;
        }
    }
}