using UnityEngine;

namespace EndlessHeresy.Runtime.Services.Camera
{
    public interface ICameraService
    {
        public UnityEngine.Camera MainCamera { get; }

        public void SetTarget(Transform target);
    }
}