using System.Threading.Tasks;
using DG.Tweening;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Actors.Builder;
using EndlessHeresy.Runtime.Actors.Enemies;
using EndlessHeresy.Runtime.Actors.Hero;
using EndlessHeresy.Runtime.Attributes;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Items;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Inventory;
using EndlessHeresy.Runtime.Movement;
using EndlessHeresy.Runtime.Scopes.Gameplay.Services.StaticData;
using EndlessHeresy.Runtime.States;
using EndlessHeresy.Runtime.States.Aggregator;
using EndlessHeresy.Runtime.States.Aggregator.Plugins;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.Vfx;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Scopes.Gameplay.Services.Factory
{
    public sealed class GameplayFactoryService : IGameplayFactoryService
    {
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        private readonly IObjectResolver _container;

        public GameplayFactoryService(IGameplayStaticDataService gameplayStaticDataService, IObjectResolver container)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            _container = container;
        }

        public Task<HeroActor> CreateHeroAsync(Vector2 at)
        {
            var configuration = _gameplayStaticDataService.HeroConfiguration;
            var movementComponent = new MovementComponent();
            var inputMovementComponent = new InputMovementComponent();
            var healthComponent = new HealthComponent();
            var facingComponent = new FacingComponent();
            var mouseFacingComponent = new MouseFacingComponent();
            var statesAggregatorComponent = new StatesAggregatorComponent<HeroActor>();
            var abilitiesCastComponent = new AbilitiesCastComponent();
            var abilitiesStorageComponent = new AbilitiesStorageComponent();
            var trailsComponent = new TrailsSpawnerComponent();
            var inventoryComponent = new InventoryComponent();
            var statsContainer = new StatsContainer();
            var statusEffectsComponent = new StatusEffectsComponent();
            var healthChangeMessages = new HealthChangeMessages();
            var attributesComponent = new AttributesComponent();
            var statModifiersComponent = new StatModifiersComponent();
            
            statsContainer.SetStats(configuration.DefaultStats);
            statModifiersComponent.SetContainer(statsContainer);
            trailsComponent.SetSize(configuration.TrailsPoolData.DefaultCapacity, configuration.TrailsPoolData.MaxSize);
            abilitiesStorageComponent.SetAbilities(configuration.AbilityConfigurations);
            inventoryComponent.SetMaxSize(configuration.MaxInventorySize);
            var statesAggregator = GetStatesAggregatorBuilder<HeroActor>()
                .WithPlugin<HeroTransitionsPlugin>()
                .WithPlugin<LoggerPlugin<HeroActor>>()
                .Build();

            statesAggregatorComponent.SetSource(statesAggregator);
            return GetActorBuilder<HeroActor>()
                .ForPrefab(configuration.Prefab)
                .WithPosition(at)
                .WithComponent(movementComponent)
                .WithComponent(healthComponent)
                .WithComponent(facingComponent)
                .WithComponent(attributesComponent)
                .WithComponent(trailsComponent)
                .WithComponent(statesAggregatorComponent)
                .WithComponent(abilitiesCastComponent)
                .WithComponent(abilitiesStorageComponent)
                .WithComponent(inventoryComponent)
                .WithComponent(mouseFacingComponent)
                .WithComponent(statsContainer)
                .WithComponent(statusEffectsComponent)
                .WithComponent(inputMovementComponent)
                .WithComponent(healthChangeMessages)
                .WithComponent(statModifiersComponent)
                .Build();
        }

        public Task<PunchingDummyActor> CreateDummyAsync(Vector2 at)
        {
            var configuration = _gameplayStaticDataService.PunchingDummyConfiguration;
            var healthComponent = new HealthComponent();
            var healthChangeMessages = new HealthChangeMessages();
            var statsContainer = new StatsContainer();
            var statusEffectsComponent = new StatusEffectsComponent();
            var statModifiersComponent = new StatModifiersComponent();

            statsContainer.SetStats(configuration.StatsData);
            statModifiersComponent.SetContainer(statsContainer);

            return GetActorBuilder<PunchingDummyActor>()
                .ForPrefab(configuration.Prefab)
                .WithPosition(at)
                .WithComponent(statsContainer)
                .WithComponent(statusEffectsComponent)
                .WithComponent(healthComponent)
                .WithComponent(healthChangeMessages)
                .WithComponent(statModifiersComponent)
                .Build();
        }

        public Task<ItemPickUpActor> CreateItemPickUpAsync(ItemType itemType)
        {
            var derivedConfiguration = _gameplayStaticDataService.GetItemConfiguration(itemType);

            if (derivedConfiguration is not PickableItemConfiguration itemConfiguration)
            {
                return Task.FromResult<ItemPickUpActor>(null);
            }

            var prefab = itemConfiguration.PickUpPrefab;

            return GetActorBuilder<ItemPickUpActor>()
                .ForPrefab(prefab)
                .WithPosition(Vector2.one)
                .Build();
        }

        public void Dispose(IActor actor)
        {
            DOTween.Kill(actor);
            actor.Dispose();
        }

        private MonoActorBuilder<TActor> GetActorBuilder<TActor>() where TActor : MonoActor
        {
            var componentsLocator = new ComponentsLocator();
            var builder = new MonoActorBuilder<TActor>(componentsLocator, _container);
            return builder;
        }

        private StatesAggregatorBuilder<TContext> GetStatesAggregatorBuilder<TContext>()
            where TContext : class, IStateMachineContext
        {
            var statesAggregatorBuilder = new StatesAggregatorBuilder<TContext>(_container);
            return statesAggregatorBuilder;
        }
    }
}