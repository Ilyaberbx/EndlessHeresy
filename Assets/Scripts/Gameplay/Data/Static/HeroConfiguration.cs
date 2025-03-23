using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Actors/Hero", fileName = "HeroConfiguration", order = 0)]
    public sealed class HeroConfiguration : ScriptableObject
    {
        [SerializeField] private HeroActor _prefab;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _health;
        [SerializeField] private AbilityConfiguration[] _abilityConfigurations;
        [SerializeField] private PoolData _trailsPoolData;

        public HeroActor Prefab => _prefab;
        public float MovementSpeed => _movementSpeed;
        public int Health => _health;
        public AbilityConfiguration[] AbilityConfigurations => _abilityConfigurations;
        public PoolData TrailsPoolData => _trailsPoolData;
    }
}