using System;
using EndlessHeresy.Runtime.Commands;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class CommandAssetInstaller : ICommandInstaller
    {
        [SerializeField] private CommandAsset _asset;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return _asset.GetCommand(resolver);
        }
    }
}