using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities.Dash
{
    [CreateAssetMenu(menuName = "Configs/Ability/Dash", fileName = "DashAbilityConfiguration", order = 0)]
    public sealed class DashConfiguration : AbilityConfiguration
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashLength;
        [SerializeField] private float _dashDamage;

        public AnimationCurve Curve => _curve;
        public float DashSpeed => _dashSpeed;
        public float DashLength => _dashLength;
        public float DashDamage => _dashDamage;
        public override AbilityBuilder GetBuilder() => new DashBuilder(this);
    }
}