using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class DecelerationStatusEffect : BaseStatusEffect
    {
        private readonly float _decelerationRate;
        private float _appliedValue;

        public DecelerationStatusEffect(float decelerationRate) => _decelerationRate = decelerationRate;

        public override void Apply(StatsComponent stats)
        {
            var moveSpeedStat = stats.GetOrAdd(StatType.MoveSpeed);
            var currentMoveSpeed = moveSpeedStat.Value;
            var newMoveSpeed = currentMoveSpeed * _decelerationRate;
            moveSpeedStat.Value = (int)newMoveSpeed;
        }

        public override void Remove(StatsComponent stats)
        {
            var moveSpeedStat = stats.GetOrAdd(StatType.MoveSpeed);
            var currentMoveSpeed = moveSpeedStat.Value;
            var newMoveSpeed = currentMoveSpeed / _decelerationRate;
            moveSpeedStat.Value = (int)newMoveSpeed;
        }
    }
}