using EndlessHeresy.Runtime.Stats;
using UnityEngine;

namespace EndlessHeresy.Runtime.StatusEffects.Implementations
{
    public sealed class VfxEffectComponent : IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect
    {
        private readonly Animator _fxAnimatorPrefab;
        private Animator _fxAnimator;
        public VfxEffectComponent(Animator fxAnimatorPrefab) => _fxAnimatorPrefab = fxAnimatorPrefab;

        public void Apply(StatsComponent stats)
        {
            var ownerTransform = stats.Owner.Transform;
            _fxAnimator = CreateVfx(ownerTransform);
        }

        public void Remove(StatsComponent stats) => Object.Destroy(_fxAnimator.gameObject);

        private Animator CreateVfx(Transform ownerTransform) =>
            Object.Instantiate(_fxAnimatorPrefab, ownerTransform.position, Quaternion.identity,
                ownerTransform);
    }
}