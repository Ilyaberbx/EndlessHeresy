using System.Collections.Generic;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Actors.Hero;
using EndlessHeresy.Runtime.Data.Persistant;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Actors/Hero", fileName = "HeroConfiguration", order = 0)]
    public sealed class HeroConfiguration : ScriptableObject
    {
        [SerializeField] private StatData[] _defaultStats;
        [SerializeField] private AttributeData[] _defaultAttributes;
        [SerializeField] private HeroActor _prefab;
        [SerializeField, Range(0, 50)] private int _maxInventorySize;
        [SerializeField] private AbilityConfiguration[] _abilityConfigurations;
        [SerializeField] private PoolData _trailsPoolData;

        public HeroActor Prefab => _prefab;
        public IReadOnlyList<AbilityConfiguration> AbilityConfigurations => _abilityConfigurations;
        public PoolData TrailsPoolData => _trailsPoolData;
        public int MaxInventorySize => _maxInventorySize;
        public IReadOnlyList<StatData> DefaultStats => _defaultStats;
        public IReadOnlyList<AttributeData> DefaultAttributes => _defaultAttributes;
    }
}