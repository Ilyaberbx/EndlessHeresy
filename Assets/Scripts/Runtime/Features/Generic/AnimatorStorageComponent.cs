using UnityEngine;

namespace EndlessHeresy.Runtime.Generic
{
    public sealed class AnimatorStorageComponent : MonoComponent
    {
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
    }
}