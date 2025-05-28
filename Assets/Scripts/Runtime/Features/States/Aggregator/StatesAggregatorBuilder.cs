using System.Collections.Generic;
using EndlessHeresy.Runtime.States.Aggregator.Plugins;
using VContainer;

namespace EndlessHeresy.Runtime.States.Aggregator
{
    public sealed class StatesAggregatorBuilder<TContext> where TContext : class, IStateMachineContext
    {
        private readonly List<StatesAggregatorPlugin<TContext>> _plugins = new();
        private readonly IObjectResolver _resolver;

        public StatesAggregatorBuilder(IObjectResolver resolver) => _resolver = resolver;

        public StatesAggregatorBuilder<TContext> WithPlugin<TPlugin>()
            where TPlugin : StatesAggregatorPlugin<TContext>, new()
        {
            var plugin = new TPlugin();
            _resolver.Inject(plugin);
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