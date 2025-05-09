using System;

namespace EndlessHeresy.Gameplay.Animations.CrescentStrike
{
    public sealed class StrikeSpinningAnimation : Animation
    {
        public event Action OnTick;

        private static readonly int IsSpinningKey = UnityEngine.Animator.StringToHash("StrikeIsSpinning");

        public void SetSpinning(bool isSpinning) => Animator.SetBool(IsSpinningKey, isSpinning);
        public bool IsSpinning => Animator.GetBool(IsSpinningKey);
        public void OnSpinAnimationTicked() => OnTick?.Invoke();
    }
}