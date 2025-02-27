using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Animations
{
    public abstract class Animation : MonoComponent
    {
        protected Animator Animator { get; private set; }

        public void SetAnimator(Animator animator)
        {
            Animator = animator;
        }

        public abstract void Play();
    }
}