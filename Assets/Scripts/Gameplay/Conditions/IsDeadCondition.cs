using System;
using Better.Conditions.Runtime;
using EndlessHeresy.Gameplay.Health;

namespace EndlessHeresy.Gameplay.Conditions
{
    public class IsDeadCondition : Condition
    {
        private readonly HealthComponent _healthComponent;

        public IsDeadCondition(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        public override bool Invoke()
        {
            return _healthComponent.CurrentHp <= 0;
        }

        protected override bool Validate(out Exception exception)
        {
            if (_healthComponent == null)
            {
                exception = new ArgumentNullException(nameof(_healthComponent));
                return false;
            }

            exception = null;
            return true;
        }
    }
}