using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.Generic;
using EndlessHeresy.Runtime.Vfx;
using UnityEngine;

namespace EndlessHeresy.Runtime.Commands.Vfx
{
    public sealed class ShowTrail : ICommand
    {
        private readonly Color _color;
        private readonly float _lifeTime;

        public ShowTrail(Color color, float lifeTime)
        {
            _color = color;
            _lifeTime = lifeTime;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var at = actor.Transform.position;
            var trailsSpawner = actor.GetComponent<TrailsSpawnerComponent>();
            var spriteRendererStorage = actor.GetComponent<SpriteRendererComponent>();
            var query = new SpawnTrailQuery(_lifeTime, _color, at, spriteRendererStorage.SpriteRenderer.sprite, "Trail");
            trailsSpawner.SpawnTrailsAsync(query).Forget();
            return Task.CompletedTask;
        }
    }
}