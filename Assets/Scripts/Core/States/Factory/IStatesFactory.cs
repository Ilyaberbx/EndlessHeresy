namespace EndlessHeresy.Core.States.Factory
{
    public interface IStatesFactory<in TDerivedState> where TDerivedState : new()
    {
        public TState GetState<TState>() where TState : TDerivedState, new();
    }
}