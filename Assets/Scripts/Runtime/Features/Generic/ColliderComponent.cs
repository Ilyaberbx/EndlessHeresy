using EndlessHeresy.Runtime.Actors;
using UnityEngine;

namespace EndlessHeresy.Runtime.Generic
{
    public abstract class ColliderComponent<TColliderComponent> : MonoComponent where TColliderComponent : Collider2D
    {
        [SerializeField] private TColliderComponent _collider;

        public TColliderComponent Collider => _collider;
    }
}