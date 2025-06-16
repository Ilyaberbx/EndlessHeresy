using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Animations;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class PlayAnimationInstaller : CommandInstaller
    {
        [SerializeField] private string _animationName;

        public override ICommand GetCommand()
        {
            return new PlayAnimation(_animationName);
        }
    }
}