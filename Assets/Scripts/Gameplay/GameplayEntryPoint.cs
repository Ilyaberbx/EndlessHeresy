using System;
using Better.Commons.Runtime.Utility;
using EndlessHeresy.Gameplay.Services.Factory;
using UnityEngine;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay
{
    public sealed class GameplayEntryPoint : IStartable
    {
        private readonly IGameplayFactoryService _gameplayFactoryService;

        public GameplayEntryPoint(IGameplayFactoryService gameplayFactoryService)
        {
            _gameplayFactoryService = gameplayFactoryService;
        }

        public async void Start()
        {
            try
            {
                await _gameplayFactoryService.CreateHeroAsync(Vector2.zero);
                await _gameplayFactoryService.CreateDummyAsync(Vector2.zero);
            }
            catch (Exception e)
            {
                DebugUtility.LogException(e);
            }
        }
    }
}