using System.Threading.Tasks;
using Better.StateMachine.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Core.Builder;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.States;
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
            var builder = GetBuilder<HeroActor>();
            var configuration = _gameplayStaticDataService.HeroConfiguration;
            var movementComponent = new MovementComponent();
            var healthComponent = new HealthComponent();
            var statesAggregator = new StatesAggregator<HeroActor>();
            var facingComponent = new FacingComponent();

            healthComponent.Setup(configuration.Health);
            statesAggregator.Setup(new StateMachine<BaseState<HeroActor>>());
            movementComponent.SetSpeed(configuration.MovementSpeed);

            return builder
                .ForPrefab(configuration.Prefab)
                .WithPosition(at)
                .WithComponent(movementComponent)
                .WithComponent(healthComponent)
                .WithComponent(statesAggregator)
                .WithComponent(facingComponent)
                .Build();
        }

        private MonoActorBuilder<TActor> GetBuilder<TActor>() where TActor : MonoActor
        {
            var componentsLocator = new ComponentsLocator();
            var builder = new MonoActorBuilder<TActor>(componentsLocator, _container);
            return builder;
        }
    }
}