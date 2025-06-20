using System;
using Better.Conditions.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Conditions
{
    [Serializable]
    public sealed class IsHoldingCondition : Condition
    {
        [SerializeField] private InputActionReference _actionReference;

        public override bool Invoke()
        {
            return _actionReference.action.ReadValue<float>() > 0f;
        }

        protected override bool Validate(out Exception exception)
        {
            if (_actionReference == null)
            {
                exception = new ArgumentNullException(nameof(_actionReference));
                return false;
            }

            exception = null;
            return true;
        }
    }
}