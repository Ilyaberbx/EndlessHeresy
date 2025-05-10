using EndlessHeresy.Gameplay.Stats;
using UnityEngine;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class VfxStatusEffectComponent : IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect
    {
        private readonly Animator _fxAnimatorPrefab;
        private Animator _fxAnimator;
        public VfxStatusEffectComponent(Animator fxAnimatorPrefab) => _fxAnimatorPrefab = fxAnimatorPrefab;

        public void Apply(StatsContainer stats)
        {
            var ownerTransform = stats.Owner.Transform;
            _fxAnimator = CreateVfx(ownerTransform);
        }

        public void Remove(StatsContainer stats) => Object.Destroy(_fxAnimator.gameObject);

        private Animator CreateVfx(Transform ownerTransform) =>
            Object.Instantiate(_fxAnimatorPrefab, ownerTransform.position, Quaternion.identity,
                ownerTransform);
    }
}