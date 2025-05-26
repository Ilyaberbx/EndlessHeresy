using System;
using Better.Conditions.Runtime;

namespace EndlessHeresy.Runtime.Abilities.Conditions
{
    public sealed class ActiveAbilityCondition : Condition
    {
        private readonly AbilitiesCastComponent _castComponent;
        private readonly bool _state;

        public ActiveAbilityCondition(AbilitiesCastComponent castComponent, bool state)
        {
            _castComponent = castComponent;
            _state = state;
        }

        public override bool Invoke() => _castComponent.HasActiveAbility() == _state;

        protected override bool Validate(out Exception exception)
        {
            if (_castComponent == null)
            {
                exception = new ArgumentNullException(nameof(_castComponent));
                return false;
            }

            exception = null;
            return true;
        }
    }
}