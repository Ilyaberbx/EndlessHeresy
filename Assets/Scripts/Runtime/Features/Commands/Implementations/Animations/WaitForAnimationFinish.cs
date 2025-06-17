using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Generic;

namespace EndlessHeresy.Runtime.Commands.Animations
{
    public sealed class WaitForAnimationFinish : ICommand
    {
        private const int MaxWaitTime = 10;

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var animator = actor.GetComponent<AnimatorStorageComponent>().Animator;
            if (animator == null)
            {
                return Task.CompletedTask;
            }

            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            var length = stateInfo.length;
            var waitTask = Task.Delay((int)(length * 1000), cancellationToken);
            var timeOutTask = Task.Delay(MaxWaitTime * 1000, cancellationToken);
            return Task.WhenAny(waitTask, timeOutTask);
        }
    }
}