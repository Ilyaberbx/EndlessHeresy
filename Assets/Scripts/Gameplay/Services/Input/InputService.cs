using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Input
{
    public sealed class InputService : IInputService
    {
        private bool _isLocked;

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        public Vector2 GetMovementInput()
        {
            float horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
            float vertical = UnityEngine.Input.GetAxisRaw("Vertical");
            return new Vector2(horizontal, vertical);
        }

        public Vector2 GetMousePosition() => UnityEngine.Input.mousePosition;
        public bool GetKeyDown(KeyCode key) => !_isLocked && UnityEngine.Input.GetKeyDown(key);
        public bool GetMouseButton(int button) => !_isLocked && UnityEngine.Input.GetMouseButton(button);
    }
}