using System;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Animations;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Wait For Animation Event", story: "Wait for [AnimationEvent] on [Actor]",
        category: "Action/EndlessHeresy", id: "537b5f9df977bebdf6a177165e34b1c5")]
    public partial class WaitForAnimationEventAction : Action
    {
        [SerializeReference] public BlackboardVariable<string> AnimationEvent;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;
        private AnimationsEventListener _eventsListener;
        private TaskCompletionSource<bool> _tcs;
        private const float MaxWaitTime = 10f;

        protected override Status OnStart()
        {
            _eventsListener = Actor.Value.GetComponent<AnimationsEventListener>();

            if (_eventsListener == null)
            {
                return Status.Failure;
            }

            _tcs = new TaskCompletionSource<bool>();

            _eventsListener.Register(AnimationEvent.Value, OnAnimationEventTriggered);

            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(MaxWaitTime));
            var completionTask = Task.WhenAny(_tcs.Task, timeoutTask);

            completionTask.ContinueWith(_ =>
            {
                _eventsListener.Unregister(AnimationEvent.Value, OnAnimationEventTriggered);
            }).Forget();

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_tcs.Task.IsCompleted)
            {
                return _tcs.Task.IsCanceled ? Status.Failure : Status.Success;
            }

            return Status.Running;
        }

        protected override void OnEnd()
        {
            _eventsListener?.Unregister(AnimationEvent.Value, OnAnimationEventTriggered);
        }

        private void OnAnimationEventTriggered()
        {
            _tcs.TrySetResult(true);
        }
    }
}