using EndlessHeresy.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace EndlessHeresy.Gameplay.Movement.Rotate
{
    public sealed class RotationAroundStorage : MonoComponent
    {
        [FormerlySerializedAs("Source")] [FormerlySerializedAs("Around")] public Transform Origin;
    }
}