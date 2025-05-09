using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Gameplay.Data.Identifiers;

namespace EndlessHeresy.Gameplay.Stats
{
    public interface IStatsReadonly
    {
        public ReactiveProperty<int> Get(StatType identifier);
        ReactiveProperty<int>[] GetAll();
    }
}