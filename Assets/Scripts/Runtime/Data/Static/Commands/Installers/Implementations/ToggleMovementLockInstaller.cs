using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Movement;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ToggleMovementLockInstaller : ICommandInstaller
    {
        [SerializeField] private bool _isLocked;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new ToggleMovementLock(_isLocked);
        }
    }
}