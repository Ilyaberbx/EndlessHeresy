using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting;
using UnityEngine;
using VContainer;
using Debug = EndlessHeresy.Runtime.Commands.Supporting.Debug;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class DebugInstaller : ICommandInstaller
    {
        [SerializeField] private string _message;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new Debug(_message);
        }
    }
}