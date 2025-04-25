using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class TemporaryStatusEffect : StatusEffectDecorator, IUpdatableStatusEffect
    {
        private const float MaxProgress = 1;
        private readonly float _duration;
        private float _elapsedTime;

        public TemporaryStatusEffect(IStatusEffect core, float duration) : base(core)
        {
            _duration = duration;
        }

        public float GetProgress()
        {
            return MaxProgress - _elapsedTime / _duration;
        }

        public void Update(IActor owner)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _duration)
            {
                if (Root.TryGet<IdentifiedStatusEffect>(out var identifiedStatusEffect))
                {
                    owner.GetComponent<StatusEffectsComponent>().Remove(identifiedStatusEffect.Identifier);
                }
            }
        }

        public void Reset() => _elapsedTime = 0;
    }
}