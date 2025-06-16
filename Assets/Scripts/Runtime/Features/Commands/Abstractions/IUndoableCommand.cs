namespace EndlessHeresy.Runtime.Commands
{
    public interface IUndoableCommand : ICommand
    {
        ICommand GetUndoCommand();
    }
}