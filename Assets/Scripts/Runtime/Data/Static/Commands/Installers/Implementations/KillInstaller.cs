using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class KillInstaller : ICommandInstaller
    {
        public ICommand GetCommand(IObjectResolver resolver)
        {
            return new Kill();
        }
    }
}