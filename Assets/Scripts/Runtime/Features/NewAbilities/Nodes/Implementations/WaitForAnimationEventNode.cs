using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.NewAnimations;

namespace EndlessHeresy.Runtime.NewAbilities.Nodes
{
    public sealed class WaitForAnimationEventNode : AbilityNode
    {
        private const int MaxWaitTime = 10;

        private readonly string _eventName;

        public WaitForAnimationEventNode(string eventName)
        {
            _eventName = eventName;
        }

        public override async Task ExecuteAsync(AbilityContext context, CancellationToken cancellationToken)
        {
            var listener = context.Caster?.GetComponent<AnimationsEventListener>();

            if (listener == null)
            {
                throw new InvalidOperationException(
                    "WaitForAnimationEventNode: Caster does not have an AnimationsEventListener.");
            }

            var tcs = new TaskCompletionSource<bool>();
            listener.Register(_eventName, Callback);
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(MaxWaitTime), cancellationToken);
            await Task.WhenAny(tcs.Task, timeoutTask);
            listener.Unregister(_eventName, Callback);
            return;
            void Callback() => tcs.TrySetResult(true);
        }
    }
}