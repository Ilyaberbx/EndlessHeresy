using EndlessHeresy.Runtime.Utilities;

namespace EndlessHeresy.Runtime.Scopes.Global.States
{
    public sealed class GameplayState : BaseLoadingState
    {
        protected override string GetSceneName() => SceneNamesUtility.Gameplay;
    }
}