using EndlessHeresy.Runtime.Commands;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    public interface ICommandInstaller
    {
        public ICommand GetCommand();
    }
}