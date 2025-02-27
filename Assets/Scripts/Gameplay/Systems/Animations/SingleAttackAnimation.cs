using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Animations
{
    public sealed class SingleAttackAnimation : Animation
    {
        private static readonly int SingleAttackHash = Animator.StringToHash("SingleAttack");
        public event Action OnAttackTriggered;
        public event Action OnAttackFinished;
        private void OnAttackAnimationEvent() => OnAttackTriggered?.Invoke();
        private void OnAttackFinishEvent() => OnAttackFinished?.Invoke();
        public override void Play() => Animator.SetTrigger(SingleAttackHash);
    }
}