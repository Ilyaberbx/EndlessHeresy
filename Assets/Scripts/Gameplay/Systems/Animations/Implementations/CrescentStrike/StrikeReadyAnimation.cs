using System;

namespace EndlessHeresy.Gameplay.Animations.CrescentStrike
{
    public sealed class StrikeReadyAnimation : Animation
    {
        public event Action OnEnded;

        private static readonly int Ready = UnityEngine.Animator.StringToHash("StrikeReady");

        public void OnReadyAnimationEnd() => OnEnded?.Invoke();
        public void Play() => Animator.SetTrigger(Ready);
    }
}