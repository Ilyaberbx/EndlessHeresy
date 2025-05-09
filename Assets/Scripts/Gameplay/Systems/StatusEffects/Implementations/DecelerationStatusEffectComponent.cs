using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class DecelerationStatusEffectComponent : 
        IStatusEffectComponent, 
        IApplyStatusEffect,
        IRemoveStatusEffect
    {
        private readonly float _decelerationRate;
        private float _appliedValue;

        public DecelerationStatusEffectComponent(float decelerationRate) => _decelerationRate = decelerationRate;

        public void Apply(StatsComponent stats)
        {
            var moveSpeedStat = stats.GetOrAdd(StatType.MoveSpeed);
            var currentMoveSpeed = moveSpeedStat.Value;
            var newMoveSpeed = currentMoveSpeed * _decelerationRate;
            moveSpeedStat.Value = (int)newMoveSpeed;
        }

        public void Remove(StatsComponent stats)
        {
            var moveSpeedStat = stats.GetOrAdd(StatType.MoveSpeed);
            var currentMoveSpeed = moveSpeedStat.Value;
            var newMoveSpeed = currentMoveSpeed / _decelerationRate;
            moveSpeedStat.Value = (int)newMoveSpeed;
        }
    }
}