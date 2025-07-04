using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Sprites;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D.Animation;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class SetSpriteAssetInstaller : UndoableCommandInstaller
    {
        [SerializeField] private SpriteLibraryAsset _asset;
        [SerializeField] private AnimatorLayerType _layerIdentifer;

        public override IUndoableCommand GetUndoableCommand()
        {
            return new SetSpriteAssetCommand(_asset, _layerIdentifer);
        }

        public override ICommand GetCommand()
        {
            return GetUndoableCommand();
        }
    }
}