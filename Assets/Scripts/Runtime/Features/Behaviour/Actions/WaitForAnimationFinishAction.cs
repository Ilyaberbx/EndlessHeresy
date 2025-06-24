using System;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Wait For Animation Finish", story: "Wait for animation finished on [Actor]",
        category: "Action/EndlessHeresy", id: "5cdc95a6dea20b69416e29a933f96651")]
    public partial class WaitForAnimationFinishAction : Action
    {
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        private TaskCompletionSource<bool> _tcs;
        private const int MaxWaitTime = 10;

        protected override Status OnStart()
        {
            var animator = Actor.Value.GetComponent<AnimatorStorageComponent>().Animator;

            if (animator == null)
            {
                return Status.Failure;
            }

            _tcs = new TaskCompletionSource<bool>();

            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            var length = stateInfo.length;

            var waitTask = Task.Delay((int)(length * 1000));
            var timeOutTask = Task.Delay(MaxWaitTime * 1000);

            Task.WhenAny(waitTask, timeOutTask)
                .ContinueWith(t => { _tcs.TrySetResult(t.Result == waitTask); })
                .Forget();

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_tcs.Task.IsCompleted)
            {
                return _tcs.Task.Result ? Status.Success : Status.Failure;
            }

            return Status.Running;
        }
    }
}