using System;
using Better.Conditions.Runtime;
using EndlessHeresy.Runtime.Generic;
using UnityEngine;

namespace EndlessHeresy.Runtime.Conditions
{
    [Serializable]
    public sealed class IsMovingCondition : Condition, IActorCondition
    {
        [SerializeField] private float _minMagnitude = 0.01f;

        private Rigidbody2D _rigidbody;

        public void Initialize(IActor actor)
        {
            _rigidbody = actor.GetComponent<RigidbodyStorageComponent>().Rigidbody;
        }

        public override bool Invoke()
        {
            return _rigidbody.linearVelocity.magnitude > _minMagnitude;
        }

        protected override bool Validate(out Exception exception)
        {
            if (_rigidbody == null)
            {
                exception = new ArgumentNullException(nameof(_rigidbody));
                return false;
            }

            exception = null;
            return true;
        }
    }
}