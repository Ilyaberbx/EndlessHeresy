using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Animations;

namespace EndlessHeresy.Runtime.Commands.Animations
{
    public sealed class WaitForAnimationEvent : ICommand
    {
        private const float MaxWaitTime = 10;

        private readonly string _eventName;

        public WaitForAnimationEvent(string eventName)
        {
            _eventName = eventName;
        }

        public async Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var listener = actor.GetComponent<AnimationsEventListener>();
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