using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting.For;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public abstract class IteractionCommandInstaller : ICommandInstaller
    {
        public abstract IIterationCommand GetIterationCommand();
        public abstract ICommand GetCommand(IObjectResolver resolver);
    }
}