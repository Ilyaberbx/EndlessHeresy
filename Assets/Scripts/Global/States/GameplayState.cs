using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Gameplay.Services.Factory;
using UnityEngine;

namespace EndlessHeresy.Global.States
{
    public sealed class GameplayState : BaseLoadingState
    {
        private GameFactoryService _gameFactoryService;

        protected override async Task OnSceneLoaded()
        {
            await base.OnSceneLoaded();

            _gameFactoryService = ServiceLocator.Get<GameFactoryService>();

            await _gameFactoryService.CreateHeroAsync(Vector2.zero);
        }

        protected override string GetSceneName() => SceneConstants.Gameplay;
    }
}