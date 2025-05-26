using UnityEngine;

namespace EndlessHeresy.Runtime.Camera
{
    public interface ICameraService
    {
        public UnityEngine.Camera MainCamera { get; }

        public void SetTarget(Transform target);
    }
}