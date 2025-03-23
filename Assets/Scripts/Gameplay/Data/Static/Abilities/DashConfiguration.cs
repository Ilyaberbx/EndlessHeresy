using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Data.Static.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/Dash", fileName = "DashAbilityConfiguration", order = 0)]
    public sealed class DashConfiguration : AbilityConfiguration
    {
        [SerializeField] private KeyCode _keyCode;
        [SerializeField] private int _cooldown;
        [SerializeField] private int _force;
        [SerializeField] private int _collisionForce;
        [SerializeField] private TrailData _trailData;
        [SerializeField] private float _trailsRatio;

        public KeyCode KeyCode => _keyCode;
        public int Cooldown => _cooldown;
        public int Force => _force;
        public int CollisionForce => _collisionForce;
        public TrailData TrailData => _trailData;
        public float TrailsRatio => _trailsRatio;
        public override AbilityFactory GetFactory(IObjectResolver container) => new DashFactory(container, this);
    }
}