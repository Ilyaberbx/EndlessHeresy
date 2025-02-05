using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Animations
{
    public sealed class SingleAttackAnimation : Animation
    {
        private static readonly int SingleAttackHash = Animator.StringToHash("SingleAttack");

        public event Action OnAttackTriggered;

        public bool IsAttackFinished { get; private set; }

        private void OnAttackAnimationEvent()
        {
            OnAttackTriggered?.Invoke();
        }

        private void OnAttackFinishEvent()
        {
            IsAttackFinished = true;
        }

        public override void Play()
        {
            IsAttackFinished = false;
            Animator.SetTrigger(SingleAttackHash);
        }
    }
}