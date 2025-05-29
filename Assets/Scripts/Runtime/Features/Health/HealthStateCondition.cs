using System;
using Better.Conditions.Runtime;

namespace EndlessHeresy.Runtime.Health
{
    public class HealthStateCondition : Condition
    {
        private readonly HealthComponent _healthComponent;
        private readonly bool _state;

        public HealthStateCondition(HealthComponent healthComponent, bool state)
        {
            _healthComponent = healthComponent;
            _state = state;
        }

        public override bool Invoke()
        {
            var isAlive = _healthComponent.CurrentHealthProperty.Value > 0;
            return isAlive == _state;
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