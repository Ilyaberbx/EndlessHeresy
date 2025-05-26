using System;
using Better.Conditions.Runtime;
using VContainer;

namespace EndlessHeresy.Runtime.Input.Conditions
{
    public sealed class IsMousePressed : Condition
    {
        private readonly int _mouseIndex;
        private IInputService _inputService;

        public IsMousePressed(int mouseIndex) => _mouseIndex = mouseIndex;

        [Inject]
        public void Construct(IInputService inputService) => _inputService = inputService;

        public override bool Invoke() => _inputService.GetMouseButtonDown(_mouseIndex);

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