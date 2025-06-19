using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Facing;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ToggleFacingLockInstaller : ICommandInstaller
    {
        [SerializeField] private bool _isLocked;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new ToggleFacingLock(_isLocked);
        }
    }
}