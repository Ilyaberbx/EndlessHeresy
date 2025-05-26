using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Utility;
using Better.StateMachine.Runtime;
using EndlessHeresy.Runtime.States.Aggregator.Plugins;

namespace EndlessHeresy.Runtime.States.Aggregator
{
    public sealed class StatesAggregator<TContext> : IStatesAggregator<TContext> where TContext : IStateMachineContext
    {
        private const string PluginAlreadyInstalledMessage = "Plugin already installed";

        private readonly HashSet<StatesAggregatorPlugin<TContext>> _plugins = new();
        public IEnumerable<StatesAggregatorPlugin<TContext>> Plugins => _plugins;
        public TContext Context { get; private set; }
        public event Action<TContext> OnContextChanged;
        public IStateMachine<BaseState<TContext>> StateMachine { get; } = new StateMachine<BaseState<TContext>>();

        public void Install(StatesAggregatorPlugin<TContext> plugin)
        {
            if (plugin == null)
            {
                DebugUtility.LogException<NullReferenceException>();
                return;
            }

            var isAdded = _plugins.Add(plugin);

            if (isAdded)
            {
                plugin.Install(this);
                return;
            }

            DebugUtility.LogException<InvalidOperationException>(PluginAlreadyInstalledMessage);
        }

        public void Uninstall(StatesAggregatorPlugin<TContext> plugin)
        {
            if (_plugins.Remove(plugin))
            {
                plugin.Uninstall(this);
            }
        }

        public void SetContext(TContext context)
        {
            if (context == null)
            {
                return;
            }

            Context = context;
            OnContextChanged?.Invoke(context);
        }
    }
}