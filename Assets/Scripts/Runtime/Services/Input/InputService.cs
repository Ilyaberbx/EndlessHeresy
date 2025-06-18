using System;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Services.Input
{
    public sealed class InputService : IInputService, IInitializable
    {
        public event Action<InputActionMap> OnActiveMapChanged;
        private GameActions _gameActions;
        public GameActions.GameplayActions GameplayActions => _gameActions.Gameplay;

        public void Initialize()
        {
            _gameActions = new GameActions();
            SetActiveMap(GameplayActions);
        }

        public void SetActiveMap(InputActionMap map)
        {
            if (map.enabled)
            {
                return;
            }

            _gameActions.Disable();
            map.Enable();
            OnActiveMapChanged?.Invoke(map);
        }
    }
}