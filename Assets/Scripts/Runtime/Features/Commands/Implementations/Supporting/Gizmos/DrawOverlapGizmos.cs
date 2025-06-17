using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Generic;
using UnityEngine;
using UnityGizmos = UnityEngine.Gizmos;

namespace EndlessHeresy.Runtime.Commands.Supporting.Gizmos
{
    public sealed class DrawOverlapGizmos : ICommand
    {
        private readonly Vector2 _at;
        private readonly CapsuleOverlapData _data;
        private const float GizmoVisibleTime = 2f;
        private float _gizmoTimer = 0f;

        public DrawOverlapGizmos(Vector2 at, CapsuleOverlapData data)
        {
            _at = at;
            _data = data;
        }

        public async Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var drawGizmosObserver = actor.GetComponent<DrawGizmosObserver>();
            if (drawGizmosObserver == null)
            {
                Debug.LogWarning("No DrawGizmosObserver component found on actor.");
                return;
            }

            await Task.Yield();

            drawGizmosObserver.OnDrawGizmosTriggered += DrawGizmos;

            while (_gizmoTimer < GizmoVisibleTime && !cancellationToken.IsCancellationRequested)
            {
                _gizmoTimer += Time.deltaTime;
                await Task.Yield();
            }

            drawGizmosObserver.OnDrawGizmosTriggered -= DrawGizmos;
        }

        private void DrawGizmos()
        {
            if (_gizmoTimer >= GizmoVisibleTime)
            {
                return;
            }

            UnityGizmos.color = Color.green;
            UnityGizmos.DrawWireCube(_at + _data.Center, new Vector3(_data.Size.x, _data.Size.y, 0));
            UnityGizmos.DrawRay(_at + _data.Center, Vector3.up * _data.Size.y / 2);
        }
    }
}