using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Services.Factory;
using UnityEngine;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay
{
    public sealed class GameplayEntryPoint : IAsyncStartable
    {
        private readonly IGameplayFactoryService _gameplayFactoryService;

        public GameplayEntryPoint(IGameplayFactoryService gameplayFactoryService)
        {
            _gameplayFactoryService = gameplayFactoryService;
        }

        public async Task StartAsync(CancellationToken cancellation = default)
        {
            await _gameplayFactoryService.CreateHeroAsync(Vector2.zero);
            await _gameplayFactoryService.CreateDummyAsync(Vector2.zero);
        }
    }
}