using EndlessHeresy.Runtime.Actors;
using UnityEngine;

namespace EndlessHeresy.Runtime.Generic
{
    public sealed class SpriteRendererComponent : MonoComponent
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
    }
}