using System;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Services.Input
{
    public interface IInputService
    {
        event Action<InputActionMap> OnActiveMapChanged;
        GameActions.GameplayActions GameplayActions { get; }
        void SetActiveMap(InputActionMap map);
    }
}