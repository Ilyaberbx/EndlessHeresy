using System;
using Better.Conditions.Runtime;
using EndlessHeresy.Runtime.Services.Input;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Input
{
    public abstract class KeyCondition : Condition
    {
        protected IInputService InputService { get; private set; }
        protected KeyCode KeyCode { get; private set; }
        protected KeyCondition(KeyCode keyCode) => KeyCode = keyCode;

        [Inject]
        public void Construct(IInputService inputService) => InputService = inputService;

        protected override bool Validate(out Exception exception)
        {
            if (InputService == null)
            {
                exception = new ArgumentNullException(nameof(InputService));
                return false;
            }

            exception = null;
            return true;
        }
    }
}