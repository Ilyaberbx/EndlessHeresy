using EndlessHeresy.Helpers;

namespace EndlessHeresy.Global.States
{
    public sealed class GameplayState : BaseLoadingState
    {
        protected override string GetSceneName() => SceneNamesHelper.Gameplay;
    }
}