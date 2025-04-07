using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components.Stats
{
    [Serializable]
    public abstract class RangeStatData<TValue> : BaseStatData<TValue>
    {
        [SerializeField] private TValue _minValue;
        [SerializeField] private TValue _maxValue;

        public TValue MinValue => _minValue;
        public TValue MaxValue => _maxValue;
    }
}