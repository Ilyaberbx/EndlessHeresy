using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Gameplay.Stats;
using EndlessHeresy.UI.MVC;

namespace EndlessHeresy.UI.Huds.Stats
{
    public sealed class StatsHudModel : IModel
    {
        public IStatsReadonly Stats { get; }
        public StatsHudModel(IStatsReadonly stats) => Stats = stats;
        public ReactiveProperty<int>[] GetAllStats() => Stats.GetAll();
    }
}