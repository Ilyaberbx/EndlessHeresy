using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Gameplay.Data.Identifiers;

namespace EndlessHeresy.Gameplay.Stats.Modifiers
{
    public interface IStatModifiersReadonly
    {
        ReadOnlyReactiveProperty<int> GetProcessedStat(StatType identifier);
    }
}