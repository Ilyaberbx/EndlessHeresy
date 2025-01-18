using UnityEngine;
using UnityEngine.Serialization;

namespace EndlessHeresy.Gameplay.Abilities.Dash
{
    [CreateAssetMenu(menuName = "Configs/Ability/Dash", fileName = "DashAbilityConfiguration", order = 0)]
    public sealed class DashAbilityConfiguration : AbilityConfiguration
    {
        [SerializeField] private AnimationCurve _dashCurve;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashLength;
        [SerializeField] private float _dashDamage;

        public AnimationCurve DashCurve => _dashCurve;
        public float DashSpeed => _dashSpeed;
        public float DashLength => _dashLength;
        public float DashDamage => _dashDamage;
        public override AbilityBuilder GetBuilder() => new DashAbilityBuilder(this);
    }
}