using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Core.Builder;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Services.StaticData;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    [Serializable]
    public sealed class GameFactoryService : PocoService, IGameFactoryService
    {
        private GameplayStaticDataService _gameplayStaticDataService;
        private HeroConfiguration _heroConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            _gameplayStaticDataService = ServiceLocator.Get<GameplayStaticDataService>();
            _heroConfiguration = _gameplayStaticDataService.HeroConfiguration;
        }

        public Task<HeroActor> CreateHeroAsync(Vector2 at)
        {
            var componentsLocator = new ComponentsLocator();
            var heroBuilder = new MonoActorBuilder<HeroActor>(componentsLocator);

            var abilitiesStorage = new AbilityStorageComponent();
            abilitiesStorage.Configure(_heroConfiguration.Abilities);
            var heroMovementComponent = new HeroMovementComponent();
            heroMovementComponent.Configure(_heroConfiguration.MovementSpeed);
            var abilityCast = new AbilityCastComponent();

            return heroBuilder
                .ForPrefab(_heroConfiguration.Prefab)
                .WithPosition(at)
                .WithParent(null)
                .WithComponent(abilityCast)
                .WithComponent(abilitiesStorage)
                .WithComponent(heroMovementComponent)
                .Build();
        }
    }
}