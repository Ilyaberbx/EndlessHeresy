using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Facing;
using UnityEngine;
using UnityEngine.Pool;

namespace EndlessHeresy.Runtime.Vfx
{
    public sealed class TrailsSpawnerComponent : PocoComponent
    {
        private FacingComponent _facingComponent;
        private IObjectPool<SpriteRenderer> _trailPool;
        private readonly int _defaultCapacity;
        private readonly int _maxSize;

        public TrailsSpawnerComponent(int defaultCapacity, int maxSize)
        {
            _defaultCapacity = defaultCapacity;
            _maxSize = maxSize;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _facingComponent = Owner.GetComponent<FacingComponent>();

            _trailPool = new ObjectPool<SpriteRenderer>(
                CreateTrail,
                OnTrailGet,
                OnTrailRelease,
                OnTrailDestroy,
                collectionCheck: false,
                defaultCapacity: _defaultCapacity,
                maxSize: _maxSize
            );

            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _trailPool.Clear();
        }

        public async Task SpawnTrailsAsync(SpawnTrailQuery query)
        {
            var trailRenderer = _trailPool.Get();
            trailRenderer.color = query.Color;
            trailRenderer.transform.position = query.At;
            trailRenderer.sprite = query.Sprite;
            trailRenderer.name = query.Name;

            var fadeTask = trailRenderer
                .DOFade(0, query.LifeTime)
                .AsTask(DisposalToken);

            await fadeTask;
            _trailPool.Release(trailRenderer);
        }

        private SpriteRenderer CreateTrail()
        {
            var trail = new GameObject();
            var trailRenderer = trail.AddComponent<SpriteRenderer>();
            return trailRenderer;
        }

        private void OnTrailGet(SpriteRenderer trailRenderer)
        {
            if (trailRenderer == null) return;
            trailRenderer.gameObject.SetActive(true);
            trailRenderer.flipX = !_facingComponent.IsFacingRight;
            trailRenderer.color = Color.white;
        }

        private void OnTrailRelease(SpriteRenderer trailRenderer)
        {
            trailRenderer.gameObject.SetActive(false);
            trailRenderer.color = Color.clear;
        }

        private void OnTrailDestroy(SpriteRenderer trailRenderer) => Object.Destroy(trailRenderer.gameObject);
    }
}