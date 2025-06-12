using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Generic;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public sealed class PlayAnimationNode : AbilityNode
    {
        private readonly string _animationName;

        public PlayAnimationNode(string animationName)
        {
            _animationName = animationName;
        }

        public override Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken)
        {
            var caster = context.Caster;
            var animator = caster.GetComponent<AnimationsStorageComponent>().Animator;
            animator.Play(_animationName);
            return Task.CompletedTask;
        }
    }
}