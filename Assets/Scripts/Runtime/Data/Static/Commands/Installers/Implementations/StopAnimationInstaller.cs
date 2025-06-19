using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Animations;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class StopAnimationInstaller : ICommandInstaller
    {
        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new StopAnimation();
        }
    }
}