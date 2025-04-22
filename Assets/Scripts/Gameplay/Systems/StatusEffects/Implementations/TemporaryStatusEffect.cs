using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class TemporaryStatusEffect : StatusEffectDecorator, IUpdatableStatusEffect
    {
        private readonly float _duration;
        private float _elapsedTime;

        public TemporaryStatusEffect(IStatusEffect core, float duration) : base(core)
        {
            _duration = duration;
        }

        public void Update(IActor owner)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _duration)
            {
                owner.GetComponent<StatusEffectsComponent>().Remove(this);
            }
        }

        public void Reset() => _elapsedTime = 0;
    }
}