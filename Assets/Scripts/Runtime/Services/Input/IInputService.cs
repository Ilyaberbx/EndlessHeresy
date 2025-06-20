using System;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Services.Input
{
    public interface IInputService
    {
        event Action<InputActionMap> OnActiveMapChanged;
        void SetActiveMap(InputActionMap activeMap);
    }
}