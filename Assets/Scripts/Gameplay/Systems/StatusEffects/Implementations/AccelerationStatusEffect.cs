using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class AccelerationStatusEffect : BaseStatusEffect
    {
        private readonly float _accelerationRate;
        private float _appliedValue;

        public AccelerationStatusEffect(float accelerationRate) => _accelerationRate = accelerationRate;

        public override void Apply(StatsComponent stats)
        {
            var moveSpeedStat = stats.GetOrAdd(StatType.MoveSpeed);
            var currentMoveSpeed = moveSpeedStat.Value;
            var newMoveSpeed = currentMoveSpeed * _accelerationRate;
            moveSpeedStat.Value = (int)newMoveSpeed;
        }

        public override void Remove(StatsComponent stats)
        {
            var moveSpeedStat = stats.GetOrAdd(StatType.MoveSpeed);
            var currentMoveSpeed = moveSpeedStat.Value;
            var newMoveSpeed = currentMoveSpeed / _accelerationRate;
            moveSpeedStat.Value = (int)newMoveSpeed;
        }
    }
}