using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.StatusEffects
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/StatusEffects/Deceleration",
        fileName = "DecelerationStatusEffectConfiguration", order = 0)]
    public sealed class DecelerationStatusEffectConfiguration : StatusEffectConfiguration
    {
        private const float MinDecelerationRate = 0f;
        private const float MaxDecelerationRate = 1f;

        [SerializeField, Range(MinDecelerationRate, 1f)] private float _decelerationRate;
        [SerializeField, Range(1f, 100f)] private float _progressionFactor;
        [SerializeField] private float _duration;


        public override IStatusEffect GetStatusEffect()
        {
            var stackable = new StackableStatusEffect(GetEffectsProgression);
            var temporary = new TemporaryStatusEffect(stackable, _duration);
            var stackDurationSync = new StackDurationSynchronizer(stackable, temporary);
            var root = new IdentifiedStatusEffect(Identifier, stackDurationSync);
            stackable.SetRoot(root);
            temporary.SetRoot(root);
            stackDurationSync.SetRoot(root);
            return root;
        }

        private IStatusEffect GetEffectsProgression(int stack)
        {
            var rate = _decelerationRate * stack * _progressionFactor;
            rate = Mathf.Clamp(rate, MinDecelerationRate, MaxDecelerationRate);
            return new DecelerationStatusEffect(rate);
        }
    }
}