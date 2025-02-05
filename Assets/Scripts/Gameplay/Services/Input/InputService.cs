using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Input
{
    [Serializable]
    public sealed class InputService : PocoService, IInputService
    {
        private bool _isLocked;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

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