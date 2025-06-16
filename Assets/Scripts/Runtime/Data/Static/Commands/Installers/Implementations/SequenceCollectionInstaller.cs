using System;
using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class SequenceCollectionInstaller : CommandInstaller
    {
        [SerializeReference, Select] private CommandInstaller[] _commandInstallers;

        public override ICommand GetCommand()
        {
            return new SequenceCollection(_commandInstallers.Select(temp => temp.GetCommand()).ToArray());
        }
    }
}