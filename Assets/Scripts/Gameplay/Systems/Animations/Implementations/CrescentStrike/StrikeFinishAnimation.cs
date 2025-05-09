using System;

namespace EndlessHeresy.Gameplay.Animations.CrescentStrike
{
    public sealed class StrikeFinishAnimation : Animation
    {
        public event Action OnEnded;
        public event Action OnHit;

        private static readonly int FinishTrigger = UnityEngine.Animator.StringToHash("StrikeFinish");

        public void OnFinishAnimationEnd() => OnEnded?.Invoke();
        public void OnFinishHit() => OnHit?.Invoke();

        public void Play() => Animator.Play(FinishTrigger);
    }
}