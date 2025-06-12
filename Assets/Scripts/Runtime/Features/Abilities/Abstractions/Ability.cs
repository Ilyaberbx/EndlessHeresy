using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Conditions.Runtime;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Abilities
{
    public abstract class Ability : IDisposable
    {
        private Condition _condition;
        private AbilityType _identifier;

        public Condition Condition => _condition;
        public AbilityType Identifier => _identifier;
        public ReactiveProperty<AbilityState> State { get; } = new();
        protected IActor Owner { get; private set; }

        public virtual void Initialize(IActor owner)
        {
            Owner = owner;
        }

        public virtual void Dispose()
        {
        }

        public abstract Task UseAsync(CancellationToken token);
        public void SetCondition(Condition condition) => _condition = condition;
        public void SetType(AbilityType type) => _identifier = type;
        protected void SetState(AbilityState state) => State.Value = state;
    }
}