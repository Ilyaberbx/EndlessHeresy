namespace EndlessHeresy.Gameplay.Services.Camera
{
    public sealed class CameraService : ICameraService
    {
        public UnityEngine.Camera MainCamera { get; }

        public CameraService(UnityEngine.Camera mainCamera) => MainCamera = mainCamera;
    }
}