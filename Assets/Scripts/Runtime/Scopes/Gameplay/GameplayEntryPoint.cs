using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Actors.Hero;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Services.Camera;
using EndlessHeresy.Runtime.Services.Gameplay.Factory;
using UnityEngine;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Scopes.Gameplay
{
    public sealed class GameplayEntryPoint : IPostInitializable, IDisposable
    {
        private readonly IGameplayFactoryService _gameplayFactoryService;
        private readonly ICameraService _cameraService;
        private readonly List<MonoActor> _activeActors;

        public GameplayEntryPoint(IGameplayFactoryService gameplayFactoryService, ICameraService cameraService)
        {
            _gameplayFactoryService = gameplayFactoryService;
            _cameraService = cameraService;
            _activeActors = new List<MonoActor>();
        }

        public async void PostInitialize()
        {
            try
            {
                await CreatDummiesAsync();
                var hero = await CreateHero();
                _activeActors.Add(hero);
                _cameraService.SetTarget(hero.transform);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }


        public void Dispose()
        {
            foreach (var actor in _activeActors)
            {
                _gameplayFactoryService.Dispose(actor);
            }
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
                var dummy = await _gameplayFactoryService.CreateDummyAsync(Vector2
                    .zero
                    .AddX(posOffset)
                    .AddY(posOffset));
                _activeActors.Add(dummy);
            }
        }
    }
}