using System;

namespace EndlessHeresy.Runtime.Animations.CrescentStrike
{
    public sealed class StrikeReadyAnimation : BaseAnimation
    {
        public event Action OnEnded;

        private static readonly int Ready = UnityEngine.Animator.StringToHash("StrikeReady");

        public void OnReadyAnimationEnd() => OnEnded?.Invoke();
        public void Play() => Animator.SetTrigger(Ready);
    }
}