using System;
using System.Linq;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting.For;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ForLoopInstaller : CommandInstaller
    {
        [SerializeField] private int _count;
        [SerializeReference, Select] private IteractionCommandInstaller[] _iteractionInstallers;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new ForLoop(_iteractionInstallers.Select(temp => temp.GetIterationCommand()).ToArray(), _count);
        }
    }
}