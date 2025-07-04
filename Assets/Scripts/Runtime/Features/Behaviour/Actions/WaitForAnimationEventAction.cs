using System;
using EndlessHeresy.Runtime.Animations;
using EndlessHeresy.Runtime.Data.Static.AnimationLayers;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Wait For Animation Event",
        story: "Wait for [AnimationEvent] on [Actor] with [LayersSelector]",
        category: "Action/EndlessHeresy", id: "537b5f9df977bebdf6a177165e34b1c5")]
    public partial class WaitForAnimationEventAction : Action
    {
        [SerializeReference] public BlackboardVariable<string> AnimationEvent;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;
        [SerializeReference] public BlackboardVariable<AnimationLayerSelectorAsset> LayersSelector;

        private AnimationsEventListener _eventsListener;
        private int _countToWait;
        private int _currentCount;

        protected override Status OnStart()
        {
            _currentCount = 0;
            _countToWait = LayersSelector.Value.LayerIdentifiers.Length;
            _eventsListener = Actor.Value.GetComponent<AnimationsEventListener>();

            if (_eventsListener == null)
            {
                return Status.Failure;
            }

            _eventsListener.Register(AnimationEvent.Value, OnAnimationEventTriggered);
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_currentCount < _countToWait)
            {
                return Status.Running;
            }

            _eventsListener?.Unregister(AnimationEvent.Value, OnAnimationEventTriggered);
            return Status.Success;
        }

        private void OnAnimationEventTriggered()
        {
            _currentCount++;
        }
    }
}