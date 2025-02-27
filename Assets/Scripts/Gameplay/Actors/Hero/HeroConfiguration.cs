using EndlessHeresy.Gameplay.Abilities;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Hero
{
    [CreateAssetMenu(menuName = "Configs/Actors/Hero", fileName = "HeroConfiguration", order = 0)]
    public sealed class HeroConfiguration : ScriptableObject
    {
        [SerializeField] private HeroActor _prefab;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _health;
        [SerializeField] private AbilityConfiguration[] _abilityConfigurations;
        
        public HeroActor Prefab => _prefab;
        public float MovementSpeed => _movementSpeed;
        public int Health => _health;
        public AbilityConfiguration[] AbilityConfigurations => _abilityConfigurations;
    }
}