using Better.StateMachine.Runtime;
using Better.StateMachine.Runtime.Modules.Transitions;
using EndlessHeresy.Core.States;

namespace EndlessHeresy.Core.StatesAggregator.Plugins
{
    public abstract class TransitionsPlugin<TContext> : StatesAggregatorPlugin<TContext>
        where TContext : IStateMachineContext
    {
        private AutoTransitionsModule<BaseState<TContext>> _transitionsModule;

        protected sealed override void InstallInternally(IStatesAggregator<TContext> statesAggregator)
        {
            var stateMachine = statesAggregator.StateMachine;

            if (StatesFactory == null)
            {
                return;
            }

            if (stateMachine.TryGetModule(out _transitionsModule))
            {
                return;
            }

            _transitionsModule = new AutoTransitionsModule<BaseState<TContext>>();
            stateMachine.AddModule(_transitionsModule);
            statesAggregator.OnContextChanged += OnContextChanged;
        }

        protected override void UninstallInternally(IStatesAggregator<TContext> statesAggregator)
        {
            base.UninstallInternally(statesAggregator);

            statesAggregator.OnContextChanged -= OnContextChanged;
        }

        private void OnContextChanged(TContext context) => ConfigureTransitions(_transitionsModule);

        protected abstract void ConfigureTransitions(TransitionsModule<BaseState<TContext>> transitionsModule);
    }
}