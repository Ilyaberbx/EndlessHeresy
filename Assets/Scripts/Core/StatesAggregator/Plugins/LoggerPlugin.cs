using EndlessHeresy.Commons;
using EndlessHeresy.Core.States;

namespace EndlessHeresy.Core.StatesAggregator.Plugins
{
    public sealed class LoggerPlugin<TContext> : StatesAggregatorPlugin<TContext> where TContext : IStateMachineContext
    {
        protected override void InstallInternally(IStatesAggregator<TContext> statesAggregator)
        {
            var stateMachine = statesAggregator.StateMachine;

            stateMachine.TryAddModule(new LoggerModule<BaseState<TContext>>());
        }
    }
}