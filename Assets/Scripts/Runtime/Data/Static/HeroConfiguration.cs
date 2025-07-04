using System.Collections.Generic;
using EndlessHeresy.Runtime.Actors.Hero;
using EndlessHeresy.Runtime.Behaviour.Events;
using EndlessHeresy.Runtime.Data.Static.Abilities;
using EndlessHeresy.Runtime.Data.Static.AnimationLayers;
using EndlessHeresy.Runtime.Data.Static.Components;
using Unity.Behavior.GraphFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Actors/Hero", fileName = "HeroConfiguration", order = 0)]
    public sealed class HeroConfiguration : ScriptableObject
    {
        [SerializeField] private SerializableGUID _abilityToCastGuid;
        [SerializeField] private OnHeroStateChanged _statesChangeChannel;
        [SerializeField] private OnAbilityUsageFinished _abilityUsageFinishedChannel;
        [SerializeField] private AnimationLayerSelectorAsset _movementLayersSelectorAsset;
        [SerializeField] private EquipmentSwordData[] _equipmentSwordDefaultData;
        [SerializeField] private InputActionReference _movementInputData;
        [SerializeField] private InputActionReference _toggleCheatsInputData;
        [SerializeField] private InputActionReference _toggleInventoryInputData;
        [SerializeField] private AbilityInputData[] _abilitiesInputData;
        [SerializeField] private AbilityConfiguration[] _abilityConfigurations;
        [SerializeField] private StatData[] _defaultStats;
        [SerializeField] private HeroActor _prefab;
        [SerializeField, Range(0, 50)] private int _maxInventorySize;
        [SerializeField] private PoolData _trailsPoolData;

        public HeroActor Prefab => _prefab;
        public PoolData TrailsPoolData => _trailsPoolData;
        public int MaxInventorySize => _maxInventorySize;
        public IReadOnlyList<StatData> DefaultStats => _defaultStats;
        public IReadOnlyList<AbilityConfiguration> AbilityConfigurations => _abilityConfigurations;
        public IReadOnlyList<AbilityInputData> AbilitiesInputData => _abilitiesInputData;
        public InputAction MovementInputData => _movementInputData.action;
        public InputAction ToggleCheatsInputData => _toggleCheatsInputData.action;
        public OnHeroStateChanged StatesChangeChannel => _statesChangeChannel;
        public SerializableGUID AbilityToCastGuid => _abilityToCastGuid;
        public OnAbilityUsageFinished AbilityUsageFinishedChannel => _abilityUsageFinishedChannel;
        public InputAction ToggleInventoryInputData => _toggleInventoryInputData.action;
        public AnimationLayerSelectorAsset MovementLayersSelectorAsset => _movementLayersSelectorAsset;
        public IReadOnlyList<EquipmentSwordData> EquipmentSwordDefaultData => _equipmentSwordDefaultData;
    }
}