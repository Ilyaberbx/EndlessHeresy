using System;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Services.Input
{
    public sealed class InputService : IInputService, IInitializable
    {
        public event Action<InputActionMap> OnActiveMapChanged;
        private readonly PlayerInput _input;

        public InputService(PlayerInput input)
        {
            _input = input;
        }

        public void SetActiveMap(InputActionMap activeMap)
        {
            if (activeMap.enabled)
            {
                return;
            }

            foreach (var map in _input.actions.actionMaps)
            {
                map.Disable();
            }

            activeMap.Enable();
            OnActiveMapChanged?.Invoke(activeMap);
        }

        public void Initialize()
        {
            _input.actions.Enable();
        }
    }
}