using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Generic;
using UnityEngine;

namespace EndlessHeresy.Runtime.Facing
{
    public sealed class FacingComponent : PocoComponent
    {
        private SpriteRendererComponent _spriteRendererComponent;
        private Type _padlock;
        private SpriteRenderer SpriteRenderer => _spriteRendererComponent.SpriteRenderer;
        private bool IsLocked { get; set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _spriteRendererComponent = Owner.GetComponent<SpriteRendererComponent>();
            return Task.CompletedTask;
        }

        public bool IsFacingRight => !SpriteRenderer.flipX;

        public void Lock()
        {
            if (IsLocked)
            {
                return;
            }

            IsLocked = true;
        }

        public void Unlock()
        {
            if (!IsLocked)
            {
                return;
            }

            IsLocked = false;
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