namespace EndlessHeresy.Runtime.Scopes.Global.States.Factory
{
    public interface IGameStatesFactory
    {
        public BaseGameState Create<TState>() where TState : BaseGameState, new();
    }
}