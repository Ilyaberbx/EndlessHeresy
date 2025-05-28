using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.StateMachine.Runtime;
using EndlessHeresy.Runtime.States.Aggregator.Plugins;

namespace EndlessHeresy.Runtime.States.Aggregator
{
    public sealed class StatesAggregatorComponent<TContext> : PocoComponent, IStatesAggregator<TContext>
        where TContext : IStateMachineContext
    {
        public event Action<TContext> OnContextChanged
        {
            add => _statesAggregator.OnContextChanged += value;
            remove => _statesAggregator.OnContextChanged -= value;
        }

        private readonly IStatesAggregator<TContext> _statesAggregator;
        public TContext Context => _statesAggregator.Context;
        public IStateMachine<BaseState<TContext>> StateMachine => _statesAggregator.StateMachine;
        public IEnumerable<StatesAggregatorPlugin<TContext>> Plugins => _statesAggregator.Plugins;
        public void SetContext(TContext context) => _statesAggregator.SetContext(context);
        public void Install(StatesAggregatorPlugin<TContext> plugin) => _statesAggregator.Install(plugin);
        public void Uninstall(StatesAggregatorPlugin<TContext> plugin) => _statesAggregator.Uninstall(plugin);

        public StatesAggregatorComponent(StatesAggregator<TContext> statesAggregator)
        {
            _statesAggregator = statesAggregator;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            if (Owner is TContext context)
            {
                SetContext(context);
            }

            return Task.CompletedTask;
        }
    }
}