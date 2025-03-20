using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Services.Camera;
using EndlessHeresy.Gameplay.Services.Factory;
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
                _cameraService.SetTarget(hero.transform);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        private async Task<HeroActor> CreateHero()
        {
            return await _gameplayFactoryService.CreateHeroAsync(Vector2.zero);
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