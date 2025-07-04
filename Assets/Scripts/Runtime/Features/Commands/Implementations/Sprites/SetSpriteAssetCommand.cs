using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Generic;
using UnityEngine.U2D.Animation;

namespace EndlessHeresy.Runtime.Commands.Sprites
{
    public sealed class SetSpriteAssetCommand : IUndoableCommand, IActorCommand
    {
        private readonly SpriteLibraryAsset _asset;
        private readonly AnimatorLayerType _layerIdentifier;
        private SpriteLibraryAsset _cachedAsset;
        private IActor _actor;

        public SetSpriteAssetCommand(SpriteLibraryAsset asset, AnimatorLayerType layerIdentifier)
        {
            _asset = asset;
            _layerIdentifier = layerIdentifier;
        }

        public void Execute()
        {
            if (_actor == null)
            {
                return;
            }

            if (!_actor.TryGetComponent<LayerSpriteLibrary>(out var layerSpriteLibrary))
            {
                return;
            }

            layerSpriteLibrary.SetLibraryAsset(_asset, _layerIdentifier);
        }

        public void Setup(IActor actor)
        {
            _actor = actor;
        }

        public ICommand GetUndoCommand()
        {
            return new SetSpriteAssetCommand(_cachedAsset, _layerIdentifier);
        }
    }
}