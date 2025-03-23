using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public sealed class PoolData
    {
        [SerializeField] private int _defaultCapacity;
        [SerializeField] private int _maxSize;

        public int DefaultCapacity => _defaultCapacity;
        public int MaxSize => _maxSize;
    }
}