using System.Collections.Generic;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Actors/Hero", fileName = "HeroConfiguration", order = 0)]
    public sealed class HeroConfiguration : ScriptableObject
    {
        [SerializeField] private AddStatData[] _defaultStats;
        [SerializeField] private AddAttributeData[] _defaultAttributes;
        [SerializeField] private HeroActor _prefab;
        [SerializeField, Range(0, 50)] private int _maxInventorySize;
        [SerializeField] private AbilityConfiguration[] _abilityConfigurations;
        [SerializeField] private PoolData _trailsPoolData;

        public HeroActor Prefab => _prefab;
        public AbilityConfiguration[] AbilityConfigurations => _abilityConfigurations;
        public PoolData TrailsPoolData => _trailsPoolData;
        public int MaxInventorySize => _maxInventorySize;
        public IReadOnlyList<AddStatData> DefaultStats => _defaultStats;
        public IReadOnlyList<AddAttributeData> DefaultAttributes => _defaultAttributes;
    }
}