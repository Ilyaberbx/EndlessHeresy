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

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            _spriteRendererComponent = Owner.GetComponent<SpriteRendererComponent>();
        }

        public void Face(bool faceRight) => SpriteRenderer.flipX = !faceRight;
    }
}