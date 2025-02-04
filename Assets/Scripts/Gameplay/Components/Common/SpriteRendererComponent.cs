using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Common
{
    public sealed class SpriteRendererComponent : MonoComponent
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
    }
}