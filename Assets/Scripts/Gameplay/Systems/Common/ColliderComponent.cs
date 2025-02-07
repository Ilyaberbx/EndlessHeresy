using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Common
{
    public abstract class ColliderComponent<TColliderComponent> : MonoComponent where TColliderComponent : Collider2D
    {
        [SerializeField] private TColliderComponent _collider;

        public TColliderComponent Collider => _collider;
    }
}