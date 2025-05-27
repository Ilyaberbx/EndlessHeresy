using Better.Conditions.Runtime;
using Better.StateMachine.Runtime.Modules.Transitions;
using Better.StateMachine.Runtime.States;

namespace EndlessHeresy.Runtime.Extensions
{
    public static class TransitionsExtensions
    {
        public static void Any<TState>(this TransitionsModule<TState> module, TState to, Condition condition) where TState : BaseState
        {
            module.AddTransition(to, condition);
        }
    }
}