using System;
using UnityEngine;

namespace EndlessHeresy.Runtime.Animations
{
    public sealed class SingleAttackAnimation : BaseAnimation
    {
        private static readonly int SingleAttackHash = Animator.StringToHash("SingleAttack");
        public event Action OnAttackTriggered;
        public event Action OnAttackFinished;
        private void OnAttackAnimationEvent() => OnAttackTriggered?.Invoke();
        private void OnAttackFinishEvent() => OnAttackFinished?.Invoke();
        public void Play() => Animator.SetTrigger(SingleAttackHash);
    }
}