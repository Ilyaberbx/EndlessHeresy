using EndlessHeresy.Gameplay.Utilities;

namespace EndlessHeresy.Global.States
{
    public sealed class GameplayState : BaseLoadingState
    {
        protected override string GetSceneName() => SceneNamesUtility.Gameplay;
    }
}