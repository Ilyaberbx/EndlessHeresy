using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Common
{
    public sealed class RigidbodyStorageComponent : MonoComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        public Rigidbody2D Rigidbody => _rigidbody;
    }
}