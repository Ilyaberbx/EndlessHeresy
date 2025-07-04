using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public sealed class LayerAnimatorData
    {
        [SerializeField] private AnimatorLayerType _layerIdentifier;
        [SerializeField] private Animator _animator;

        public AnimatorLayerType LayerIdentifier => _layerIdentifier;
        public Animator Animator => _animator;
    }
}