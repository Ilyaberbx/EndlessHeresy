using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/Dash", fileName = "DashAbilityConfiguration", order = 0)]
    public sealed class DashConfiguration : AbilityConfiguration
    {
        [SerializeField] private KeyCode _keyCode;
        [SerializeField] private int _speed;
        [SerializeField] private float _length;
        [SerializeField] private AnimationCurve _curve;

        public int Speed => _speed;
        public float Length => _length;
        public AnimationCurve Curve => _curve;
        public KeyCode KeyCode => _keyCode;

        public override AbilityBuilder GetBuilder(IObjectResolver container) => new DashBuilder(container, this);
    }
}