using System;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class PeriodicInstaller : CommandInstaller
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _perSeconds;
        [SerializeReference, Select] private CommandInstaller _command;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new Periodic(_perSeconds, _duration, _command.GetCommand(resolver));
        }
    }
}