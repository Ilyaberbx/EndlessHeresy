using System.Collections.Generic;
using EndlessHeresy.Runtime.Actors.Hero;
using EndlessHeresy.Runtime.Behaviour.Events;
using EndlessHeresy.Runtime.Data.Persistant;
using EndlessHeresy.Runtime.Data.Static.Abilities;
using EndlessHeresy.Runtime.Data.Static.Components;
using Unity.Behavior;
using Unity.Behavior.GraphFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Actors/Hero", fileName = "HeroConfiguration", order = 0)]
    public sealed class HeroConfiguration : ScriptableObject
    {
        [SerializeField] private BlackboardReference _blackboardReference;
        [SerializeField] private SerializableGUID _abilityToCastGuid;
        [SerializeField] private OnHeroStateChanged _statesChangeChannel;
        [SerializeField] private OnAbilityUsageFinished _abilityUsageFinishedChannel;
        [SerializeField] private InputActionReference _movementInputData;
        [SerializeField] private AbilityInputData[] _abilitiesInputData;
        [SerializeField] private AbilityConfiguration[] _abilityConfigurations;
        [SerializeField] private StatData[] _defaultStats;
        [SerializeField] private AttributeData[] _defaultAttributes;
        [SerializeField] private HeroActor _prefab;
        [SerializeField, Range(0, 50)] private int _maxInventorySize;
        [SerializeField] private PoolData _trailsPoolData;

        public HeroActor Prefab => _prefab;
        public PoolData TrailsPoolData => _trailsPoolData;
        public int MaxInventorySize => _maxInventorySize;
        public IReadOnlyList<StatData> DefaultStats => _defaultStats;
        public IReadOnlyList<AttributeData> DefaultAttributes => _defaultAttributes;
        public IReadOnlyList<AbilityConfiguration> AbilityConfigurations => _abilityConfigurations;
        public IReadOnlyList<AbilityInputData> AbilitiesInputData => _abilitiesInputData;
        public InputAction MovementInputData => _movementInputData.action;
        public OnHeroStateChanged StatesChangeChannel => _statesChangeChannel;
        public BlackboardReference Blackboard => _blackboardReference;
        public SerializableGUID AbilityToCastGuid => _abilityToCastGuid;
        public OnAbilityUsageFinished AbilityUsageFinishedChannel => _abilityUsageFinishedChannel;
    }
}