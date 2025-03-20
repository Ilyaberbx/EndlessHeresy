using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Components
{
    [Serializable]
    public sealed class TrailData
    {
        [SerializeField] private Color _color;
        [SerializeField] private float _lifeTime;

        public Color Color => _color;
        public float LifeTime => _lifeTime;
    }
}