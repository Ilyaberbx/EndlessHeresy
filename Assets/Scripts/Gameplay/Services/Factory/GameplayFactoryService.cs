using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Core.Builder;
using EndlessHeresy.Core.States;
using EndlessHeresy.Core.StatesAggregator;
using EndlessHeresy.Core.StatesAggregator.Plugins;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Actors.Enemies;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Effects;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.StatesAggregator;
using EndlessHeresy.Gameplay.StatesAggregator.Plugins;
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
            var statesAggregatorComponent = new StatesAggregatorComponent<HeroActor>();
            var abilitiesCastComponent = new AbilitiesCastComponent();
            var abilitiesStorageComponent = new AbilitiesStorageComponent();
            var trailsComponent = new TrailsComponent();

            var statesAggregator = GetStatesAggregatorBuilder<HeroActor>()
                .WithPlugin<HeroTransitionsPlugin>()
                .WithPlugin<LoggerPlugin<HeroActor>>()
                .Build();

            statesAggregatorComponent.SetSource(statesAggregator);
            healthComponent.SetHealth(configuration.Health);
            movementComponent.SetSpeed(configuration.MovementSpeed);
            abilitiesStorageComponent.SetAbilities(configuration.AbilityConfigurations);

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
                .Build();
        }

        public Task<PunchingDummyActor> CreateDummyAsync(Vector2 at)
        {
            var configuration = _gameplayStaticDataService.PunchingDummyConfiguration;
            var healthComponent = new HealthComponent();

            healthComponent.SetHealth(10);

            return GetActorBuilder<PunchingDummyActor>()
                .ForPrefab(configuration.Prefab)
                .WithPosition(at)
                .WithComponent(healthComponent)
                .Build();
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