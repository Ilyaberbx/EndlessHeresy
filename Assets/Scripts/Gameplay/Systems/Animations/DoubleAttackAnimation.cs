using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Animations
{
    public sealed class DoubleAttackAnimation : Animation
    {
        private static readonly int DoubleAttackHash = Animator.StringToHash("DoubleAttack");

        public event Action OnAnimationFinished;
        public event Action OnFirstAttack;
        public event Action OnSecondAttack;

        public bool IsEventFinished { get; private set; }

        public override void Play()
        {
            IsEventFinished = false;
            Animator.SetTrigger(DoubleAttackHash);
        }

        private void OnAnimationFinishEvent()
        {
            IsEventFinished = true;
        }
    }
}