using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class AccelerationStatusEffectComponent : IStatusEffectComponent, IApplyStatusEffect, IRemoveStatusEffect
    {
        private readonly float _accelerationRate;
        private float _appliedValue;

        public AccelerationStatusEffectComponent(float accelerationRate) => _accelerationRate = accelerationRate;

        public void Apply(StatsComponent stats)
        {
            var moveSpeedStat = stats.Get(StatType.MoveSpeed);
            var currentMoveSpeed = moveSpeedStat.Value;
            var newMoveSpeed = currentMoveSpeed * _accelerationRate;
            moveSpeedStat.Value = (int)newMoveSpeed;
        }

        public void Remove(StatsComponent stats)
        {
            var moveSpeedStat = stats.Get(StatType.MoveSpeed);
            var currentMoveSpeed = moveSpeedStat.Value;
            var newMoveSpeed = currentMoveSpeed / _accelerationRate;
            moveSpeedStat.Value = (int)newMoveSpeed;
        }
    }
}