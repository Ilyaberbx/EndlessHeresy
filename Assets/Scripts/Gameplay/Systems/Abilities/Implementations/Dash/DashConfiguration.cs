using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/Dash", fileName = "DashAbilityConfiguration", order = 0)]
    public sealed class DashConfiguration : AbilityConfiguration
    {
        [SerializeField] private KeyCode _keyCode;
        [SerializeField] private int _cooldown;
        [SerializeField] private int _force;
        [SerializeField] private int _collisionForce;
        
        public KeyCode KeyCode => _keyCode;
        public int Cooldown => _cooldown;
        public int Force => _force;
        public int CollisionForce => _collisionForce;

        public override AbilityFactory GetFactory(IObjectResolver container) => new DashFactory(container, this);
    }
}