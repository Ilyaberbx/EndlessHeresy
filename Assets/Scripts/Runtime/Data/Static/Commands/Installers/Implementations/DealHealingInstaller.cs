using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Core;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class DealHealingInstaller : ICommandInstaller
    {
        [SerializeField] private float _value;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new DealHealing(_value);
        }
    }
}