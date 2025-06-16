using System;
using EndlessHeresy.Runtime.Commands;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public abstract class UndoableCommandInstaller : CommandInstaller
    {
        public abstract IUndoableCommand GetUndoableCommand();
    }
}