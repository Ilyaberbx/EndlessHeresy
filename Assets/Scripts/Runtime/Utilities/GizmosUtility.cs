using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Utilities
{
    public static class GizmosUtility
    {
        private const float Half = 0.5f;
        private const float Double = 2f;
        private static readonly Vector3 DefaultScale = Vector3.one;

        public static void DrawWireCapsule2D(CapsuleOverlapData data, Vector2 at, Color color)
        {
            var originalMatrix = Gizmos.matrix;

            var localOffset = data.Center;
            var size = data.Size;
            var direction = data.Direction;
            var angle = data.Angle;

            var position = at + localOffset;
            var rotation = Quaternion.Euler(0f, 0f, angle);

            Gizmos.matrix = Matrix4x4.TRS(position, rotation, DefaultScale);
            Gizmos.color = color;

            var isVertical = direction == CapsuleDirection2D.Vertical;
            var radius = isVertical ? size.x * Half : size.y * Half;
            var bodyLength = isVertical
                ? Mathf.Max(0f, size.y - (Double * radius))
                : Mathf.Max(0f, size.x - (Double * radius));

            if (isVertical)
            {
                var topCenter = Vector2.up * (bodyLength * Half);
                var bottomCenter = Vector2.down * (bodyLength * Half);
                var bodySize = new Vector2(size.x, bodyLength);

                Gizmos.DrawWireSphere(topCenter, radius);
                Gizmos.DrawWireSphere(bottomCenter, radius);
                Gizmos.DrawWireCube(Vector2.zero, bodySize);
            }
            else
            {
                var rightCenter = Vector2.right * (bodyLength * Half);
                var leftCenter = Vector2.left * (bodyLength * Half);
                var bodySize = new Vector2(bodyLength, size.y);

                Gizmos.DrawWireSphere(rightCenter, radius);
                Gizmos.DrawWireSphere(leftCenter, radius);
                Gizmos.DrawWireCube(Vector2.zero, bodySize);
            }

            Gizmos.matrix = originalMatrix;
            Gizmos.color = Color.white;
        }
    }
}