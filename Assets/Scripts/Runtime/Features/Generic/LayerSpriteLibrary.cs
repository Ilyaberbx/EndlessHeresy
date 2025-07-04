using System.Linq;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace EndlessHeresy.Runtime.Generic
{
    public sealed class LayerSpriteLibrary : MonoComponent
    {
        [SerializeField] private LayerSpritesLibraryData[] _data;

        public void SetLibraryAsset(SpriteLibraryAsset asset, AnimatorLayerType layerIdentifier)
        {
            var data = _data.FirstOrDefault(temp => temp.LayerIdentifier == layerIdentifier);

            if (data == null)
            {
                return;
            }

            var library = data.Library;
            library.spriteLibraryAsset = asset;
        }
    }
}