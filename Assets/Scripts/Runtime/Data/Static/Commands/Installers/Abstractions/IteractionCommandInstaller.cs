using System;
using EndlessHeresy.Runtime.Commands.Supporting.For;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public abstract class IteractionCommandInstaller : CommandInstaller
    {
        public abstract IIterationCommand GetIterationCommand();
    }
}