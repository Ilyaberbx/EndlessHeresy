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
        private SpriteRenderer SpriteRenderer => _spriteRendererComponent.SpriteRenderer;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _spriteRendererComponent = Owner.GetComponent<SpriteRendererComponent>();
            return Task.CompletedTask;
        }

        public void Face(bool faceRight) => SpriteRenderer.flipX = !faceRight;
    }
}