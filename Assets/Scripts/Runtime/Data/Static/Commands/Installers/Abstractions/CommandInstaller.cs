using System;
using EndlessHeresy.Runtime.Commands;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public abstract class CommandInstaller
    {
        public abstract ICommand GetCommand(IObjectResolver resolver);
    }
}