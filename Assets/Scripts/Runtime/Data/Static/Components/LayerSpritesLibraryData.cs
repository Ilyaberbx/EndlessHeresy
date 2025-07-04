using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public class LayerSpritesLibraryData
    {
        [SerializeField] private AnimatorLayerType _layerIdentifier;
        [SerializeField] private SpriteLibrary _library;

        public AnimatorLayerType LayerIdentifier => _layerIdentifier;
        public SpriteLibrary Library => _library;
    }
}