using System.Threading.Tasks;
using DG.Tweening;
using EndlessHeresy.Core;
using EndlessHeresy.Core.Builder;
using EndlessHeresy.Core.States;
using EndlessHeresy.Core.StatesAggregator;
using EndlessHeresy.Core.StatesAggregator.Plugins;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Actors.Enemies;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.Items;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Inventory;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.StatesAggregator;
using EndlessHeresy.Gameplay.StatesAggregator.Plugins;
using EndlessHeresy.Gameplay.Stats;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.Vfx;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Services.Factory
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
            var healthComponent = new HealthComponent();
            var facingComponent = new FacingComponent();
            var mouseFacingComponent = new MouseFacingComponent();
            var statesAggregatorComponent = new StatesAggregatorComponent<HeroActor>();
            var abilitiesCastComponent = new AbilitiesCastComponent();
            var abilitiesStorageComponent = new AbilitiesStorageComponent();
            var trailsComponent = new TrailsSpawnerComponent();
            var inventoryComponent = new InventoryComponent();
            var statsComponent = new StatsComponent();
            var statusEffectsComponent = new StatusEffectsComponent();

            var statesAggregator = GetStatesAggregatorBuilder<HeroActor>()
                .WithPlugin<HeroTransitionsPlugin>()
                .WithPlugin<LoggerPlugin<HeroActor>>()
                .Build();

            statsComponent.SetStats(configuration.DefaultStats);
            statesAggregatorComponent.SetSource(statesAggregator);
            trailsComponent.SetSize(configuration.TrailsPoolData.DefaultCapacity, configuration.TrailsPoolData.MaxSize);
            abilitiesStorageComponent.SetAbilities(configuration.AbilityConfigurations);
            inventoryComponent.SetMaxSize(configuration.MaxInventorySize);

            return GetActorBuilder<HeroActor>()
                .ForPrefab(configuration.Prefab)
                .WithPosition(at)
                .WithComponent(movementComponent)
                .WithComponent(healthComponent)
                .WithComponent(facingComponent)
                .WithComponent(trailsComponent)
                .WithComponent(statesAggregatorComponent)
                .WithComponent(abilitiesCastComponent)
                .WithComponent(abilitiesStorageComponent)
                .WithComponent(inventoryComponent)
                .WithComponent(mouseFacingComponent)
                .WithComponent(statsComponent)
                .WithComponent(statusEffectsComponent)
                .Build();
        }

        public Task<PunchingDummyActor> CreateDummyAsync(Vector2 at)
        {
            var configuration = _gameplayStaticDataService.PunchingDummyConfiguration;
            var healthComponent = new HealthComponent();
            var healthChangeMessages = new HealthChangeMessages();
            var statsComponent = new StatsComponent();

            statsComponent.SetStats(configuration.StatsData);

            return GetActorBuilder<PunchingDummyActor>()
                .ForPrefab(configuration.Prefab)
                .WithPosition(at)
                .WithComponent(statsComponent)
                .WithComponent(healthComponent)
                .WithComponent(healthChangeMessages)
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