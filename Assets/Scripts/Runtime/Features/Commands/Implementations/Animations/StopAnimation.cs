using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Generic;

namespace EndlessHeresy.Runtime.Commands.Animations
{
    public sealed class StopAnimation : ICommand
    {
        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var animator = actor.GetComponent<AnimatorStorageComponent>().Animator;
            animator.enabled = false;
            return Task.CompletedTask;
        }
    }
}