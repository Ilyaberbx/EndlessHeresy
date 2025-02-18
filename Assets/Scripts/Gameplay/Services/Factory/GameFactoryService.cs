using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Better.StateMachine.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Core.Builder;
using EndlessHeresy.Core.States;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Services.StaticData;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    [Serializable]
    public sealed class GameFactoryService : PocoService, IGameFactoryService
    {
        private IGameplayStaticDataService _gameplayStaticDataService;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
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
            var builder = new MonoActorBuilder<TActor>(componentsLocator);
            return builder;
        }
    }
}