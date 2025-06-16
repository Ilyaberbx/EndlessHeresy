using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Generic;

namespace EndlessHeresy.Runtime.Commands.Animations
{
    public sealed class PlayAnimation : ICommand
    {
        private readonly string _animationName;

        public PlayAnimation(string animationName)
        {
            _animationName = animationName;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var animator = actor.GetComponent<AnimatorStorageComponent>().Animator;
            animator.Play(_animationName);
            return Task.CompletedTask;
        }
    }
}