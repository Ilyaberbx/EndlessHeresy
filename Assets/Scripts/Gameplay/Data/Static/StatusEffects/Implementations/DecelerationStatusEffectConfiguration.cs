using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
using EndlessHeresy.Gameplay.Utilities;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.StatusEffects
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/StatusEffects/Deceleration",
        fileName = "DecelerationStatusEffectConfiguration", order = 0)]
    public sealed class DecelerationStatusEffectConfiguration : StatusEffectConfiguration
    {
        [SerializeField, Range(0f, 1f)] private float _decelerationRate;
        [SerializeField] private int _duration;
        [SerializeField] private bool _type;
        [SerializeField] private bool _isStackable;
        [SerializeField] private float _progressionValue;

        public override IStatusEffect GetStatusEffect()
        {
            var stackable = new StackableStatusEffect(GetEffectsProgression);
            var temporary = new TemporaryStatusEffect(stackable, _duration);
            return new StackDurationSynchronizer(stackable, temporary);
        }

        private IStatusEffect GetEffectsProgression(int stack)
        {
            return new DecelerationStatusEffect(stack * _progressionValue);
        }
    }
}