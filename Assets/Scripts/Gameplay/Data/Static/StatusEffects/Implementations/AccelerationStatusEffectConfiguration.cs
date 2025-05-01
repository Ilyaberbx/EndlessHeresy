using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.StatusEffects
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/StatusEffects/Acceleration",
        fileName = "AccelerationStatusEffectConfiguration", order = 0)]
    public sealed class AccelerationStatusEffectConfiguration : StatusEffectConfiguration
    {
        private const float MinAccelerationRate = 1f;
        private const float MaxAccelerationRate = 100f;

        [SerializeField, Range(MinAccelerationRate, MaxAccelerationRate)]
        private float _accelerationRate;

        [SerializeField, Range(1f, 100f)] private float _progressionFactor;
        [SerializeField] private float _duration;

        public override IStatusEffect GetStatusEffect()
        {
            var temporary = new TemporaryStatusEffect(GetEffectsProgression(1), _duration);
            var root = new IdentifiedStatusEffect(Identifier, temporary);
            temporary.SetRoot(root);
            return root;
        }

        private IStatusEffect GetEffectsProgression(int stack)
        {
            var rate = _accelerationRate * stack * _progressionFactor;
            rate = Mathf.Clamp(rate, MinAccelerationRate, MaxAccelerationRate);
            return new AccelerationStatusEffect(rate);
        }
    }
}