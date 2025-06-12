using System;
using Better.Conditions.Runtime;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.NewAbilities;

namespace EndlessHeresy.Runtime.Abilities.Conditions
{
    public sealed class AbilityCastCondition<TAbility> : Condition where TAbility : Ability
    {
        private readonly AbilitiesCastComponent _castComponent;
        private readonly AbilitiesComponent _storageComponent;

        public AbilityCastCondition(AbilitiesCastComponent castComponent,
            AbilitiesComponent storageComponent)
        {
            _castComponent = castComponent;
            _storageComponent = storageComponent;
        }

        public override bool Invoke()
        {
            if (_castComponent.HasActiveAbility())
            {
                return false;
            }

            var hasAbility = _storageComponent.TryGetAbility<TAbility>(out var ability);

            if (!hasAbility)
            {
                return false;
            }

            var abilityIsReadyCondition = new AbilityStateCondition(AbilityState.Ready, ability);
            var abilityCastCondition = ability.Condition;
            return abilityCastCondition.SafeInvoke() && abilityIsReadyCondition.SafeInvoke();
        }

        protected override bool Validate(out Exception exception)
        {
            if (_castComponent == null)
            {
                exception = new ArgumentNullException(nameof(_castComponent));
                return false;
            }

            if (_storageComponent == null)
            {
                exception = new ArgumentNullException(nameof(_storageComponent));
                return false;
            }

            exception = null;
            return true;
        }
    }
}