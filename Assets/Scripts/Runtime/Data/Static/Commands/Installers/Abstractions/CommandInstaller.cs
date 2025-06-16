using System;
using EndlessHeresy.Runtime.Commands;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public abstract class CommandInstaller
    {
        public abstract ICommand GetCommand();
    }
}