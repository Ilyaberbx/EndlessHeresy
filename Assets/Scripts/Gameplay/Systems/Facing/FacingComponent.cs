using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Common;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Facing
{
    public sealed class FacingComponent : PocoComponent
    {
        private SpriteRendererComponent _spriteRendererComponent;
        private Type _padlock;
        private SpriteRenderer SpriteRenderer => _spriteRendererComponent.SpriteRenderer;
        public bool IsLocked => _padlock != null;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _spriteRendererComponent = Owner.GetComponent<SpriteRendererComponent>();
            return Task.CompletedTask;
        }

        public bool IsFacingRight => !SpriteRenderer.flipX;

        public void Lock(Type padlock)
        {
            if (IsLocked)
            {
                return;
            }

            _padlock = padlock;
        }

        public void Unlock(Type padlock)
        {
            if (!IsLocked)
            {
                return;
            }

            if (_padlock != padlock)
            {
                return;
            }

            _padlock = null;
        }

        public void Face(Type padlock, bool faceRight)
        {
            if (IsLocked)
            {
                if (_padlock != padlock)
                {
                    return;
                }
            }

            SpriteRenderer.flipX = !faceRight;
        }
    }
}