using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Conditions.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Abilities.State;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class Ability : IDisposable
    {
        private Condition _condition;
        private AbilityState _state;
        public Condition Condition => _condition;
        public AbilityState State => _state;
        protected IActor Owner { get; private set; }
        public virtual void Initialize(IActor owner) => Owner = owner;
        public virtual void Dispose()
        {
        }

        public void SetCondition(Condition condition) => _condition = condition;
        protected void SetState(AbilityState state) => _state = state;
        public abstract Task UseAsync(CancellationToken token);
    }
}