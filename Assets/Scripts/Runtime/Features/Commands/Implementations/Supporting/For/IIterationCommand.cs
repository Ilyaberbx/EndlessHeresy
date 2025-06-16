namespace EndlessHeresy.Runtime.Commands.Supporting.For
{
    public interface IIterationCommand : ICommand
    {
        void SetIndex(int index);
    }
}