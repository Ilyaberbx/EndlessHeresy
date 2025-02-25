using System;
using Better.Commons.Runtime.Utility;
using EndlessHeresy.Core.States;
using EndlessHeresy.Core.States.Factory;
using VContainer;

namespace EndlessHeresy.Core.StatesAggregator.Plugins
{
    public abstract class StatesAggregatorPlugin<TContext> where TContext : IStateMachineContext
    {
        private IStatesAggregator<TContext> _statesAggregator;
        protected TContext Context => _statesAggregator.Context;

        protected IStatesFactory<BaseState<TContext>> StatesFactory;

        private IObjectResolver _container;

        [Inject]
        public void Construct(IObjectResolver container)
        {
            _container = container;
        }

        public void Install(IStatesAggregator<TContext> statesAggregator)
        {
            _statesAggregator = statesAggregator;

            StatesFactory = new StatesFactory<BaseState<TContext>>(_container);

            if (_statesAggregator == null)
            {
                DebugUtility.LogException<NullReferenceException>();
                return;
            }

            InstallInternally(statesAggregator);
        }

        public void Uninstall(IStatesAggregator<TContext> statesAggregator)
        {
            if (_statesAggregator == null)
            {
                return;
            }

            UninstallInternally(statesAggregator);
        }

        protected abstract void InstallInternally(IStatesAggregator<TContext> statesAggregator);

        protected virtual void UninstallInternally(IStatesAggregator<TContext> statesAggregator)
        {
        }
    }
}