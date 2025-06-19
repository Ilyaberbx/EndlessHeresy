using System;
using EndlessHeresy.Runtime.Commands;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public abstract class UndoableCommandInstaller : ICommandInstaller
    {
        public abstract IUndoableCommand GetUndoableCommand();
        public abstract ICommand GetCommand(IObjectResolver resolver);
    }
}