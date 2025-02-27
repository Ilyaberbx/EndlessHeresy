using System;
using Better.Conditions.Runtime;
using EndlessHeresy.Gameplay.Services.Input;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Conditions
{
    public sealed class IsKeyPressed : Condition
    {
        private readonly KeyCode _keyCode;
        private IInputService _inputService;

        public IsKeyPressed(KeyCode keyCode) => _keyCode = keyCode;

        [Inject]
        public void Construct(IInputService inputService) => _inputService = inputService;

        public override bool Invoke() => _inputService.GetKey(_keyCode);

        protected override bool Validate(out Exception exception)
        {
            if (_inputService == null)
            {
                exception = new ArgumentNullException(nameof(_inputService));
                return false;
            }

            exception = null;
            return true;
        }
    }
}