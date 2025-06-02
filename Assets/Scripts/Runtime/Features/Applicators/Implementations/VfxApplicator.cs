using UnityEngine;

namespace EndlessHeresy.Runtime.Applicators
{
    public sealed class VfxApplicator : IApplicator
    {
        private readonly Animator _fxAnimatorPrefab;
        private Animator _fxAnimator;

        public VfxApplicator(Animator fxAnimatorPrefab)
        {
            _fxAnimatorPrefab = fxAnimatorPrefab;
        }

        public void Apply(IActor actor)
        {
            if (_fxAnimatorPrefab != null)
            {
                _fxAnimator = Object
                    .Instantiate(_fxAnimatorPrefab, actor.Transform.position,
                        Quaternion.identity, actor.Transform);
            }
        }

        public void Remove(IActor actor)
        {
            if (_fxAnimator != null)
            {
                Object.Destroy(_fxAnimator.gameObject);
            }
        }
    }
}