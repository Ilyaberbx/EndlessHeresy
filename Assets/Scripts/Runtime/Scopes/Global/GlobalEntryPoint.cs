using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Scopes.Global.States;
using EndlessHeresy.Runtime.Services.Global.States;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Scopes.Global
{
    public sealed class GlobalEntryPoint : IPostInitializable
    {
        private readonly IGameStatesService _gameStatesService;

        public GlobalEntryPoint(IGameStatesService gameStatesService)
        {
            _gameStatesService = gameStatesService;
        }

        public void PostInitialize()
        {
            _gameStatesService.ChangeStateAsync<GameplayState>().Forget();
        }
    }
}