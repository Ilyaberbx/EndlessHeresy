using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Stats.Modifiers
{
    public interface IStatModifiersReadonly
    {
        ReadOnlyReactiveProperty<int> GetProcessedStat(StatType identifier);
    }
}