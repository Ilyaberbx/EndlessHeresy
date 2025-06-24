using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Controls
{
    public sealed class InputActionPhaseRegistry : PocoComponent
    {
        private readonly Dictionary<InputAction, InputActionPhase> _map = new();

        public void Update(InputAction action, InputActionPhase phase)
        {
            if (_map.TryAdd(action, phase))
            {
                return;
            }

            _map[action] = phase;
        }

        public bool TryGetPhase(InputAction action, out InputActionPhase phase)
        {
            return _map.TryGetValue(action, out phase);
        }

        protected override void OnDispose()
        {
            _map.Clear();
        }
    }
}