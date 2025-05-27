using UnityEngine;

namespace EndlessHeresy.Runtime.Services.Input
{
    public interface IInputService
    {
        void Lock();
        void Unlock();
        Vector2 GetMovementInput();
        Vector2 GetMousePosition();
        bool GetKeyDown(KeyCode key);
        bool GetMouseButton(int button);
        bool GetMouseButtonDown(int button);
        bool GetKey(KeyCode key);
    }
}