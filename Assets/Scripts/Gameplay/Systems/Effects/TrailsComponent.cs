using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using EndlessHeresy.Core;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Data.Components;
using EndlessHeresy.Gameplay.Facing;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Effects
{
    public sealed class TrailsComponent : PocoComponent
    {
        private const string TrailName = "Trail";

        private SpriteRendererComponent _spriteRendererStorage;
        private FacingComponent _facingComponent;

        private SpriteRenderer SelfRenderer => _spriteRendererStorage.SpriteRenderer;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnPostInitializeAsync(cancellationToken);

            _spriteRendererStorage = Owner.GetComponent<SpriteRendererComponent>();
            _facingComponent = Owner.GetComponent<FacingComponent>();
        }

        public async Task ShowTrailsAsync(TrailData data)
        {
            var actorPosition = Owner.Transform.position;
            var trailRenderer = CreateTrail(data, actorPosition);

            var fadeTask = trailRenderer
                .DOFade(0, data.LifeTime)
                .AsTask(DisposalToken);

            await fadeTask;
            trailRenderer.gameObject.Destroy();
        }

        private SpriteRenderer CreateTrail(TrailData data, Vector3 actorPosition)
        {
            var sprite = SelfRenderer.sprite;
            var trail = new GameObject(TrailName);
            var trailRenderer = trail.AddComponent<SpriteRenderer>();
            trailRenderer.sprite = sprite;
            trailRenderer.color = data.Color;
            trailRenderer.flipX = !_facingComponent.IsFacingRight;
            trail.transform.position = actorPosition;
            return trailRenderer;
        }
    }
}