using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Conditions.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Abilities.Enums;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class Ability : IDisposable
    {
        private Condition _condition;
        private AbilityType _type;

        public Condition Condition => _condition;
        public AbilityType Type => _type;
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
        public void SetType(AbilityType type) => _type = type;
        protected void SetState(AbilityState state) => State.Value = state;
    }
}