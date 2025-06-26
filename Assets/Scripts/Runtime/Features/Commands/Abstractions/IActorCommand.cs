namespace EndlessHeresy.Runtime.Commands
{
    public interface IActorCommand : ICommand
    {
        void Setup(IActor actor);
    }
}