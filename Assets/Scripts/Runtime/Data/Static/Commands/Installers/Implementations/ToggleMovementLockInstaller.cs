using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Movement;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ToggleMovementLockInstaller : CommandInstaller
    {
        [SerializeField] private bool _isLocked;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new ToggleMovementLock(_isLocked);
        }
    }
}