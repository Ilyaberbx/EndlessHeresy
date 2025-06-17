using System;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Supporting;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class KillInstaller : CommandInstaller
    {
        public override ICommand GetCommand()
        {
            return new Kill();
        }
    }
}