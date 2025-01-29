using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Core.Builder;
using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Actors.CrescentKnife;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.Movement.Rotate;
using EndlessHeresy.Gameplay.Services.StaticData;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    [Serializable]
    public sealed class GameFactoryService : PocoService, IGameFactoryService
    {
        private GameplayStaticDataService _gameplayStaticDataService;

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
            var abilitiesStorage = new AbilityStorageComponent();
            var heroMovementComponent = new HeroMovementComponent();
            var abilityCast = new AbilityCastComponent();
            var rotateAroundComponent = new RotateAroundComponent(1f);

            abilitiesStorage.Configure(configuration.Abilities);
            heroMovementComponent.Configure(configuration.MovementSpeed);

            return builder
                .ForPrefab(configuration.Prefab)
                .WithPosition(at)
                .WithComponent(abilityCast)
                .WithComponent(abilitiesStorage)
                .WithComponent(heroMovementComponent)
                .WithComponent(rotateAroundComponent)
                .Build();
        }

        public Task<CrescentKnifeActor> CreateCrescentKnifeAsync(Vector2 at, Transform parent)
        {
            var builder = GetBuilder<CrescentKnifeActor>();
            var configuration = _gameplayStaticDataService.CrescentStrikeConfiguration;

            return builder
                .ForPrefab(configuration.CrescentKnifePrefab)
                .WithPosition(at)
                .WithParent(parent)
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