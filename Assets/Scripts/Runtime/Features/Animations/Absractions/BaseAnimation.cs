using EndlessHeresy.Runtime.Actors;
using UnityEngine;

namespace EndlessHeresy.Runtime.Animations
{
    public abstract class BaseAnimation : MonoComponent
    {
        protected Animator Animator { get; private set; }

        public void SetAnimator(Animator animator)
        {
            Animator = animator;
        }
    }
}