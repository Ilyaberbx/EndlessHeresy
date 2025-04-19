using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Gameplay.Data.Identifiers;

namespace EndlessHeresy.Gameplay.Stats
{
    public interface IStatsReadonly
    {
        public ReactiveProperty<int> GetOrAdd(StatType identifier);
        ReactiveProperty<int>[] GetAll();
    }
}