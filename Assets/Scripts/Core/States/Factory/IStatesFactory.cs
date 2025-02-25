namespace EndlessHeresy.Core.States.Factory
{
    public interface IStatesFactory<in TDerivedState>
    {
        public TState CreateState<TState>() where TState : TDerivedState, new();
    }
}