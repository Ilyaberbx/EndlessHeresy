using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Animations;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class WaitForAnimationEventInstaller : CommandInstaller
    {
        [SerializeField] private string _eventName;

        public override ICommand GetCommand(IObjectResolver resolver)
        {
            return new WaitForAnimationEvent(_eventName);
        }
    }
}