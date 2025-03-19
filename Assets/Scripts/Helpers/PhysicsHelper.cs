using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Helpers
{
    public static class PhysicsHelper
    {
        public static bool TryOverlapCircleAll<TComponent>(Vector2 at, float radius, out TComponent[] components)
            where TComponent : IComponent
        {
            var colliders = Physics2D.OverlapCircleAll(at, radius);

            if (colliders.IsNullOrEmpty())
            {
                components = null;
                return false;
            }

            var results = new List<TComponent>();

            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent(out IActor actor))
                {
                    continue;
                }

                if (!actor.TryGetComponent(out TComponent component))
                {
                    continue;
                }

                results.Add(component);
            }

            components = results.ToArray();
            return results.Count > 0;
        }
    }
}