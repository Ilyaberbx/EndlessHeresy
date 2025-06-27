using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Actors.Hero;
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
        private readonly Transform _dummiesSpawnPoint;
        private readonly List<MonoActor> _activeActors;

        public GameplayEntryPoint(IGameplayFactoryService gameplayFactoryService,
            ICameraService cameraService,
            Transform dummiesSpawnPoint)
        {
            _gameplayFactoryService = gameplayFactoryService;
            _cameraService = cameraService;
            _dummiesSpawnPoint = dummiesSpawnPoint;
            _activeActors = new List<MonoActor>();
        }

        public async void PostInitialize()
        {
            try
            {
                await CreatDummiesAsync(_dummiesSpawnPoint.position);
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

        private async Task CreatDummiesAsync(Vector2 at)
        {
            const int numberOfDummies = 25;
            const float radius = 5f;
            const float angleStep = 360f / numberOfDummies;

            for (var i = 0; i < numberOfDummies; i++)
            {
                var angle = i * angleStep * Mathf.Deg2Rad;
                var xOffset = Mathf.Cos(angle) * radius;
                var yOffset = Mathf.Sin(angle) * radius;

                var dummyPosition = at + new Vector2(xOffset, yOffset);

                var dummy = await _gameplayFactoryService.CreateDummyAsync(dummyPosition);
                _activeActors.Add(dummy);
            }
        }
    }
}