using System;
using System.Collections.Generic;
using Better.StateMachine.Runtime;
using EndlessHeresy.Runtime.States.Aggregator.Plugins;

namespace EndlessHeresy.Runtime.States.Aggregator
{
    public interface IStatesAggregator<TContext> where TContext : IStateMachineContext
    {
        public TContext Context { get; }
        public event Action<TContext> OnContextChanged;
        public IStateMachine<BaseState<TContext>> StateMachine { get; }
        public IEnumerable<StatesAggregatorPlugin<TContext>> Plugins { get; }
        public void SetContext(TContext context);
        public void Install(StatesAggregatorPlugin<TContext> plugin);
        public void Uninstall(StatesAggregatorPlugin<TContext> plugin);
    }
}