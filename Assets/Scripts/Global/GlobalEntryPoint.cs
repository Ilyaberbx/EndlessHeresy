using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Global.Services.StatesManagement;
using EndlessHeresy.Global.States;
using VContainer.Unity;

namespace EndlessHeresy.Global
{
    public sealed class GlobalEntryPoint : IStartable
    {
        private readonly IGameStatesService _gameStatesService;

        public GlobalEntryPoint(IGameStatesService gameStatesService)
        {
            _gameStatesService = gameStatesService;
        }

        public void Start()
        {
            _gameStatesService.ChangeStateAsync<GameplayState>().Forget();
        }
    }
}