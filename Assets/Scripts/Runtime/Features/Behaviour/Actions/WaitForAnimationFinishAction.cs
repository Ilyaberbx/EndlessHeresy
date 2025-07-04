using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Static.AnimationLayers;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Wait For Animation Finish",
        story: "Wait for animation finished on [Actor] with [LayerSelector]",
        category: "Action/EndlessHeresy", id: "5cdc95a6dea20b69416e29a933f96651")]
    public partial class WaitForAnimationFinishAction : Action
    {
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;
        [SerializeReference] public BlackboardVariable<AnimationLayerSelectorAsset> LayerSelector;

        private List<Task> _animationTasks;
        private Task _task;

        protected override Status OnStart()
        {
            var layersToSelect = LayerSelector.Value.LayerIdentifiers;
            var animatorsAggregator = Actor.Value.GetComponent<AnimatorsAggregatorComponent>();
            _task = animatorsAggregator.WaitForAnimationsFinishAsync(CancellationToken.None, layersToSelect);
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_task.IsCompleted)
            {
                return Status.Success;
            }

            if (_task.IsFaulted || _task.IsCanceled)
            {
                return Status.Failure;
            }

            return Status.Running;
        }
    }
}