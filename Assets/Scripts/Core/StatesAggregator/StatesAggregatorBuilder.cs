using System.Collections.Generic;
using EndlessHeresy.Core.States;
using EndlessHeresy.Core.StatesAggregator.Plugins;
using VContainer;

namespace EndlessHeresy.Core.StatesAggregator
{
    public sealed class StatesAggregatorBuilder<TContext> where TContext : class, IStateMachineContext
    {
        private readonly List<StatesAggregatorPlugin<TContext>> _plugins = new();
        private readonly IObjectResolver _container;

        public StatesAggregatorBuilder(IObjectResolver container) => _container = container;

        public StatesAggregatorBuilder<TContext> WithPlugin<TPlugin>()
            where TPlugin : StatesAggregatorPlugin<TContext>, new()
        {
            var plugin = new TPlugin();
            _container.Inject(plugin);
            _plugins.Add(plugin);
            return this;
        }

        public StatesAggregator<TContext> Build()
        {
            var aggregator = new StatesAggregator<TContext>();

            foreach (var plugin in _plugins)
            {
                plugin.Install(aggregator);
            }

            return aggregator;
        }
    }
}