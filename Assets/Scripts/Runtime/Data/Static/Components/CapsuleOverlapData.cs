using System;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct CapsuleOverlapData
    {
        [SerializeField] private Vector2 _center;
        [SerializeField] private Vector2 _size;
        [SerializeField] private CapsuleDirection2D _direction;
        [SerializeField] private float _angle;

        public Vector2 Center => _center;
        public Vector2 Size => _size;
        public CapsuleDirection2D Direction => _direction;
        public float Angle => _angle;

        public CapsuleOverlapData(Vector2 center, Vector2 size, CapsuleDirection2D direction, float angle)
        {
            _center = center;
            _size = size;
            _direction = direction;
            _angle = angle;
        }
    }
}