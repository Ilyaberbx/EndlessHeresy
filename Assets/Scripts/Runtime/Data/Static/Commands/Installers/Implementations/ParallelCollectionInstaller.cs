using System;
using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ParallelCollectionInstaller : CommandInstaller
    {
        [SerializeReference, Select] private CommandInstaller[] _commandInstallers;

        public override ICommand GetCommand()
        {
            return new ParallelCollection(_commandInstallers.Select(temp => temp.GetCommand()).ToArray());
        }
    }
}