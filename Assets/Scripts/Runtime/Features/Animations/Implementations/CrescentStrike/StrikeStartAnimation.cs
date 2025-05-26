using System;

namespace EndlessHeresy.Runtime.Animations.CrescentStrike
{
    public sealed class StrikeStartAnimation : BaseAnimation
    {
        private static readonly int StartChargeHash = UnityEngine.Animator.StringToHash("StrikeStartCharging");

        public event Action OnEnded;
        public void Play() => Animator.SetTrigger(StartChargeHash);

        public void OnStartAnimationEnd() => OnEnded?.Invoke();
    }
}