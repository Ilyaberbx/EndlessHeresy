using System;
using Better.Conditions.Runtime;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Abilities.Conditions
{
    public sealed class AbilityStateCondition : Condition
    {
        private readonly AbilityState _state;
        private readonly Ability _ability;

        public AbilityStateCondition(AbilityState state, Ability ability)
        {
            _state = state;
            _ability = ability;
        }

        public override bool Invoke() => _ability.State.Value == _state;

        protected override bool Validate(out Exception exception)
        {
            if (_ability == null)
            {
                exception = new ArgumentNullException(nameof(_ability));
                return false;
            }

            exception = null;
            return true;
        }
    }
}