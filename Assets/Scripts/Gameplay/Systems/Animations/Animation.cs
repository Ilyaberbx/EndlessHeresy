using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Animations
{
    public abstract class Animation : MonoComponent
    {
        [SerializeField] private Animator _animator;

        protected Animator Animator => _animator;

        public abstract void Play();
    }
}