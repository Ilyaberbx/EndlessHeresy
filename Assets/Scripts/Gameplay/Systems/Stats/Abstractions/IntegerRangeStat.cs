using UnityEngine;

namespace EndlessHeresy.Gameplay.Stats
{
    public abstract class IntegerRangeStat : RangeStat<int>
    {
        protected override int Process(int value)
        {
            return Mathf.Clamp(value, MinValue, MaxValue);
        }
    }
}