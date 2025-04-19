using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Services.Camera;
using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.StatusEffects;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
using UnityEngine;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay
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
                hero.GetComponent<StatusEffectsComponent>()
                    .Add(new TemporaryStatusEffect(new DecelerationStatusEffect(0.5f), 5));
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
            await _gameplayFactoryService.CreateDummyAsync(Vector2.zero.AddX(-2));
            await _gameplayFactoryService.CreateDummyAsync(Vector2.zero.AddX(-4));
            await _gameplayFactoryService.CreateDummyAsync(Vector2.zero.AddY(-2));
            await _gameplayFactoryService.CreateDummyAsync(Vector2.zero.AddY(2));
        }
    }
}