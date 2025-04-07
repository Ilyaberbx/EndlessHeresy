using Better.Attributes.Runtime.Select;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Data.Static.Components.Stats;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Actors/Hero", fileName = "HeroConfiguration", order = 0)]
    public sealed class HeroConfiguration : ScriptableObject
    {
        [SerializeReference, Select] private BaseStatData[] _statsData;
        [SerializeField] private HeroActor _prefab;
        [SerializeField] private float _movementSpeed;
        [SerializeField, Range(0, 100000)] private int _health;
        [SerializeField, Range(0, 50)] private int _maxInventorySize;
        [SerializeField] private AbilityConfiguration[] _abilityConfigurations;
        [SerializeField] private PoolData _trailsPoolData;

        public HeroActor Prefab => _prefab;
        public float MovementSpeed => _movementSpeed;
        public int Health => _health;
        public AbilityConfiguration[] AbilityConfigurations => _abilityConfigurations;
        public PoolData TrailsPoolData => _trailsPoolData;
        public int MaxInventorySize => _maxInventorySize;
        public BaseStatData[] StatsData => _statsData;
    }
}