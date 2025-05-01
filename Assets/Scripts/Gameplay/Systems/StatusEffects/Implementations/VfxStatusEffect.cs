using EndlessHeresy.Gameplay.Stats;
using UnityEngine;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class VfxStatusEffect : BaseStatusEffect
    {
        private readonly Animator _fxAnimatorPrefab;
        private Animator _fxAnimator;
        public VfxStatusEffect(Animator fxAnimatorPrefab) => _fxAnimatorPrefab = fxAnimatorPrefab;

        public override void Apply(StatsComponent stats)
        {
            var ownerTransform = stats.Owner.Transform;
            _fxAnimator = CreateVfx(ownerTransform);
        }

        public override void Remove(StatsComponent stats) => Object.Destroy(_fxAnimator.gameObject);

        private Animator CreateVfx(Transform ownerTransform) =>
            Object.Instantiate(_fxAnimatorPrefab, ownerTransform.position, Quaternion.identity,
                ownerTransform);
    }
}