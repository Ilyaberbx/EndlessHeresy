using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class DebugCommandInstaller : CommandInstaller
    {
        [SerializeField] private string _message;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new DebugCommand(_message);
        }
    }
}