using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Camera
{
    public interface ICameraService
    {
        public UnityEngine.Camera MainCamera { get; }

        public void SetTarget(Transform target);
    }
}