using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Animations;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class StopAnimationInstaller : CommandInstaller
    {
        public override ICommand GetCommand()
        {
            return new StopAnimation();
        }
    }
}