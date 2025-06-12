using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Generic;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public sealed class WaitForCurrentAnimationFinishNode : AbilityNode
    {
        private const float MaxWaitTime = 10f;

        public override Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken)
        {
            var caster = context.Caster;
            var animator = caster.GetComponent<AnimationsStorageComponent>().Animator;
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            var length = stateInfo.length;
            var waitTask = Task.Delay((int)(length * 1000), cancellationToken);
            var timeOutTask = Task.Delay((int)MaxWaitTime * 1000, cancellationToken);
            return Task.WhenAny(waitTask, timeOutTask);
        }
    }
}