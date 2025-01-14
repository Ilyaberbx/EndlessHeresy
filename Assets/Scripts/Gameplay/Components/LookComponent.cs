using Better.Locators.Runtime;
using EndlessHeresy.Global.Services.Input;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Hero
{
    public sealed class LookComponent : MonoBehaviour
    {
        private InputService _inputService;
        private Transform _transform;
        private Camera _camera;

        public Vector2 NormalizedLookDirection
        {
            get
            {
                var mouseWorldPosition = GetMouseWorldPosition(_inputService.GetMousePosition());
                var rawDirection = mouseWorldPosition - _transform.position;
                return rawDirection.normalized;
            }
        }

        private Vector3 GetMouseWorldPosition(Vector2 mousePosition) => _camera.ScreenToWorldPoint(mousePosition);

        private void Start()
        {
            _inputService = ServiceLocator.Get<InputService>();
            _camera = Camera.main; //TODO: Use camera service
            _transform = transform;
        }
    }
}