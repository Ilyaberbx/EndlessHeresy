using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Helpers
{
    public static class PhysicsHelper
    {
        public static bool TryOverlapSphere<TComponent>(Vector2 at, float radius, out TComponent[] components)
            where TComponent : IComponent
        {
            Collider[] colliders = { };
            Physics.OverlapSphereNonAlloc(at, radius, colliders);
            var results = new List<TComponent>();

            if (colliders.IsNullOrEmpty())
            {
                components = null;
                return false;
            }

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