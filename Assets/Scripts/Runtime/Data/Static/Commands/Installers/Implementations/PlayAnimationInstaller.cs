using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Animations;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class PlayAnimationInstaller : ICommandInstaller
    {
        [SerializeField] private string _animationName;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new PlayAnimation(_animationName);
        }
    }
}