using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Core;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class DealFacingDirectionForceInstaller : ICommandInstaller
    {
        [SerializeField] private float _multiplier;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new DealFacingDirectionForceImpulse(_multiplier);
        }
    }
}