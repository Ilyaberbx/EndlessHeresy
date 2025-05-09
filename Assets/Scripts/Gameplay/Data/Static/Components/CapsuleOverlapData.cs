using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct CapsuleOverlapData
    {
        [SerializeField] private Vector2 center;
        [SerializeField] private Vector2 size;
        [SerializeField] private CapsuleDirection2D direction;
        [SerializeField] private float angle;

        public Vector2 Center => center;
        public Vector2 Size => size;
        public CapsuleDirection2D Direction => direction;
        public float Angle => angle;
    }
}