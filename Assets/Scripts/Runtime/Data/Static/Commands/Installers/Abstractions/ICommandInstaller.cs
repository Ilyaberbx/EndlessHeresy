using EndlessHeresy.Runtime.Commands;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    public interface ICommandInstaller
    {
        public ICommand GetCommand(IObjectResolver resolver);
    }
}