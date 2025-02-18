namespace EndlessHeresy.Global.States.Factory
{
    public interface IGameStatesFactory
    {
        public BaseGameState Create<TState>() where TState : BaseGameState, new();
    }
}