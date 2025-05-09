using System.Collections.Generic;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Utilities
{
    public static class GamePhysicsUtility
    {
        private const int BufferSize = 64;
        private static readonly Collider2D[] Buffer = new Collider2D[BufferSize];

        public static bool TryOverlapCapsuleAll<TComponent>(
            CapsuleOverlapData data,
            Vector2 at,
            out TComponent[] components)
            where TComponent : IComponent
        {
            var center = at + data.Center;

            var count = Physics2D.OverlapCapsuleNonAlloc(
                center,
                data.Size,
                data.Direction,
                data.Angle,
                Buffer
            );

            if (count == 0)
            {
                components = null;
                return false;
            }

            var results = new List<TComponent>();

            for (var i = 0; i < count; i++)
            {
                var collider = Buffer[i];
                if (!collider.TryGetComponent(out IActor actor))
                    continue;

                if (!actor.TryGetComponent(out TComponent component))
                    continue;

                results.Add(component);
            }

            components = results.ToArray();
            return results.Count > 0;
        }
    }
}