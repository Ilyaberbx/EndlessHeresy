using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Camera
{
    public interface ICameraTarget
    {
        Transform CameraFollow { get; }

        Transform CameraLookAt { get; }
    }
}