using System;

namespace EndlessHeresy.Gameplay.Stats
{
    public abstract class BaseStat
    {
    }

    public abstract class BaseStat<TValue> : BaseStat
    {
        public event Action<TValue> OnValueChanged;
        private TValue _value;

        public void SetValue(TValue value)
        {
            _value = Process(value);
            OnValueChanged?.Invoke(_value);
        }

        public TValue GetValue() => _value;
        protected abstract TValue Process(TValue value);
    }
}