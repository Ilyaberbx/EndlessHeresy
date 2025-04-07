using UnityEngine;

namespace EndlessHeresy.Gameplay.Stats
{
    public abstract class FloatRangeStat : RangeStat<float>
    {
        protected override float Process(float value)
        {
            return Mathf.Clamp(value, MinValue, MaxValue);
        }
    }
}