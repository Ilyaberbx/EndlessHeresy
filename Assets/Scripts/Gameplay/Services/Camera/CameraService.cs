using Cinemachine;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Camera
{
    public sealed class CameraService : ICameraService
    {
        private readonly CinemachineVirtualCameraBase _followCamera;
        public UnityEngine.Camera MainCamera { get; }

        public CameraService(UnityEngine.Camera mainCamera, CinemachineVirtualCameraBase followCamera)
        {
            _followCamera = followCamera;
            MainCamera = mainCamera;
        }

        public void SetTarget(Transform target)
        {
            _followCamera.Follow = target;
        }
    }
}