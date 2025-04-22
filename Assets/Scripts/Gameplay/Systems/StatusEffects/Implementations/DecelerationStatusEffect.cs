using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class DecelerationStatusEffect : IStatusEffect
    {
        private readonly float _decelerationRate;
        private float _appliedValue;

        public DecelerationStatusEffect(float decelerationRate) => _decelerationRate = decelerationRate;

        public void Apply(StatsComponent stats)
        {
            var moveSpeedStat = stats.GetOrAdd(StatType.MoveSpeed);
            var currentValue = moveSpeedStat.Value;
            _appliedValue = CalculateDeceleratedSpeed(currentValue);
            moveSpeedStat.Value = (int)_appliedValue;
        }

        public void Remove(StatsComponent stats)
        {
            var moveSpeedStat = stats.GetOrAdd(StatType.MoveSpeed);
            moveSpeedStat.Value = (int)(_appliedValue / _decelerationRate);
        }
        private float CalculateDeceleratedSpeed(int currentValue) => _decelerationRate * currentValue;
    }
}