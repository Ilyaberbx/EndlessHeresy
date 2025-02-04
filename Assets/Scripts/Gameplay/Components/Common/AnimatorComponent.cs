using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Common
{
    public sealed class AnimatorComponent : MonoComponent
    {
        [SerializeField] private Animator _animator;

        public Animator Animator => _animator;
    }
}