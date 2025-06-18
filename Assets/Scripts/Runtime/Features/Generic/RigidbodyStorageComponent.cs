using UnityEngine;

namespace EndlessHeresy.Runtime.Generic
{
    public sealed class RigidbodyStorageComponent : MonoComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        public Rigidbody2D Rigidbody => _rigidbody;
    }
}