using System;
using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Commands;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ComplexInstaller : ICommandInstaller
    {
        [SerializeReference, Select] private ICommandInstaller[] _commandInstallers;

        public ICommand GetCommand()
        {
            var commands = _commandInstallers.Select(temp => temp.GetCommand());
            return new ComplexCommand(commands);
        }
    }
}