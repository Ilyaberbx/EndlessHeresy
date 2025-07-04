using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Generic;
using UnityEngine;

namespace EndlessHeresy.Runtime.Extensions
{
    public static class AnimatorExtensions
    {
        private const int TimeoutMillisecondsDelay = 10000;

        private static List<Task> GetWaitForAnimationsFinishTasks(this IEnumerable<Animator> animators,
            CancellationToken token)
        {
            var tasks = new List<Task>();

            foreach (var animator in animators)
            {
                if (animator == null) continue;

                var eventLength = animator.GetCurrentAnimatorStateInfo(0).length;
                var waitTask = Task.Delay(Mathf.RoundToInt(eventLength * 1000), token);
                var timeoutTask = Task.Delay(TimeoutMillisecondsDelay, token);

                tasks.Add(Task.WhenAny(waitTask, timeoutTask));
            }

            return tasks;
        }

        public static Task WaitForAnimationsFinishAsync(
            this AnimatorsAggregatorComponent animatorsAggregator, CancellationToken token,
            params AnimatorLayerType[] layersToSelect)
        {
            var animators = layersToSelect
                .Select(animatorsAggregator.GetAnimatorForLayer)
                .Where(animator => animator != null)
                .ToList();

            return Task.WhenAll(GetWaitForAnimationsFinishTasks(animators, token));
        }
    }
}