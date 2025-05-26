using System;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct TrailData
    {
        [SerializeField] private Color _color;
        [SerializeField] private float _lifeTime;
        [SerializeField] private string _name;

        public Color Color => _color;
        public float LifeTime => _lifeTime;
        public string Name => _name;
    }
}