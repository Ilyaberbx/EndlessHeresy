using System;
using System.Collections.Generic;
using Better.StateMachine.Runtime;
using EndlessHeresy.Core.States;
using EndlessHeresy.Core.StatesAggregator.Plugins;

namespace EndlessHeresy.Core.StatesAggregator
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