using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.Services.StaticData;
using UnityEngine;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay
{
    public sealed class GameplayEntryPoint : IStartable
    {
        private readonly IGameplayFactoryService _gameplayFactoryService;
        private readonly IGameplayStaticDataService _gameplayStaticDataService;

        public GameplayEntryPoint(IGameplayFactoryService gameplayFactoryService,
            IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayFactoryService = gameplayFactoryService;
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        public async void Start()
        {
            await _gameplayStaticDataService.LoadHeroConfigurationAsync();
            await _gameplayFactoryService.CreateHeroAsync(Vector2.zero);
        }
    }
}