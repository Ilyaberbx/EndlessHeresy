using System;
using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ParallelCollectionInstaller : ICommandInstaller
    {
        [SerializeReference, Select] private ICommandInstaller[] _commandInstallers;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new ParallelCollection(_commandInstallers.Select(temp => temp.GetCommand(resolver)).ToArray());
        }
    }
}