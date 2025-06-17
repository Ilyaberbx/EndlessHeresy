using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class DebugCommandInstaller : CommandInstaller
    {
        [SerializeField] private string _message;

        public override ICommand GetCommand()
        {
            return new DebugCommand(_message);
        }
    }
}