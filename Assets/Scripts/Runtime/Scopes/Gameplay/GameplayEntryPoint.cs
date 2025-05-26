using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Extensions;
using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Actors.Hero;
using EndlessHeresy.Runtime.Camera;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Scopes.Gameplay.Services.Factory;
using UnityEngine;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Scopes.Gameplay
{
    public sealed class GameplayEntryPoint : IAsyncStartable
    {
        private readonly IGameplayFactoryService _gameplayFactoryService;
        private readonly ICameraService _cameraService;

        public GameplayEntryPoint(IGameplayFactoryService gameplayFactoryService, ICameraService cameraService)
        {
            _gameplayFactoryService = gameplayFactoryService;
            _cameraService = cameraService;
        }

        public async Task StartAsync(CancellationToken cancellation = default)
        {
            try
            {
                await CreatDummiesAsync();
                var hero = await CreateHero();
                _cameraService.SetTarget(hero.transform);
                CreateTestItemPickUp().Forget();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        private Task<ItemPickUpActor> CreateTestItemPickUp()
        {
            return _gameplayFactoryService.CreateItemPickUpAsync(ItemType.Test);
        }

        private Task<HeroActor> CreateHero()
        {
            return _gameplayFactoryService.CreateHeroAsync(Vector2.zero);
        }

        private async Task CreatDummiesAsync()
        {
            await _gameplayFactoryService.CreateDummyAsync(Vector2.zero);

            for (var i = 0; i < 25; i++)
            {
                var isEven = i % 2 == 0;
                var multiplier = isEven ? 1f : -1f;
                var posOffset = i * multiplier * 2;
                await _gameplayFactoryService.CreateDummyAsync(Vector2
                    .zero
                    .AddX(posOffset)
                    .AddY(posOffset));
            }
        }
    }
}