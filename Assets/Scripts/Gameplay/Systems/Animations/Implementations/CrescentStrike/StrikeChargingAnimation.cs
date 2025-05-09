using System;

namespace EndlessHeresy.Gameplay.Animations.CrescentStrike
{
    public sealed class StrikeChargingAnimation : Animation
    {
        public event Action OnTick;
        private static readonly int IsCharging = UnityEngine.Animator.StringToHash("StrikeIsCharging");
        public void SetCharging(bool isCharging) => Animator.SetBool(IsCharging, isCharging);
        public void OnChargingAnimationTicked() => OnTick?.Invoke();
    }
}