using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Animations
{
    public sealed class DoubleAttackAnimation : Animation
    {
        private static readonly int DoubleAttackHash = Animator.StringToHash("DoubleAttack");

        public event Action OnFirstHitTriggered;
        public event Action OnSecondHitTriggered;
        public event Action OnAttacksFinishTriggered;
        public event Action OnFirstDragTriggered;
        public event Action OnSecondDragTriggered;

        private void OnFirstHitEvent() => OnFirstHitTriggered?.Invoke();
        private void OnSecondHitEvent() => OnSecondHitTriggered?.Invoke();

        private void OnFirstDragEvent() => OnFirstDragTriggered?.Invoke();
        private void OnSecondDragEvent() => OnSecondDragTriggered?.Invoke();
        private void OnAttacksFinishEvent() => OnAttacksFinishTriggered?.Invoke();
        public override void Play() => Animator.SetTrigger(DoubleAttackHash);
    }
}