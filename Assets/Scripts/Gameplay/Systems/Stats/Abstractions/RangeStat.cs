namespace EndlessHeresy.Gameplay.Stats
{
    public abstract class RangeStat<TValue> : BaseStat<TValue>
    {
        protected TValue MaxValue { get; private set; }
        protected TValue MinValue { get; private set; }

        public void SetMax(TValue value) => MaxValue = value;
        public void SetMin(TValue value) => MinValue = value;
    }
}