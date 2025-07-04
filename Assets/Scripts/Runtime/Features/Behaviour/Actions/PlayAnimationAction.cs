using System;
using EndlessHeresy.Runtime.Data.Static.AnimationLayers;
using EndlessHeresy.Runtime.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Play Animation ", story: "Play [Animation] on [Actor] with [LayerSelector]",
        category: "Action/EndlessHeresy",
        id: "8c47f21655f04f2998ef09e81b78f67d")]
    public partial class PlayAnimationAction : Action
    {
        [SerializeReference] public BlackboardVariable<string> Animation;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;
        [SerializeReference] public BlackboardVariable<AnimationLayerSelectorAsset> LayerSelector;

        protected override Status OnStart()
        {
            var layersToSelect = LayerSelector.Value.LayerIdentifiers;
            var actor = Actor.Value;

            if (!actor.TryGetComponent<AnimatorsAggregatorComponent>(out var animatorsAggregator))
            {
                return Status.Failure;
            }

            animatorsAggregator.Play(Animation.Value, layersToSelect);
            return Status.Success;
        }
    }
}