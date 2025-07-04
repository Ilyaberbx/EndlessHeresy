using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.AnimationLayers
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/AnimationLayerSelector", fileName = "AnimationLayerSelectorAsset", order = 0)]
    public sealed class AnimationLayerSelectorAsset : ScriptableObject
    {
        [SerializeField] private AnimatorLayerType[] _layerIdentifiers;

        public AnimatorLayerType[] LayerIdentifiers => _layerIdentifiers;
    }
}