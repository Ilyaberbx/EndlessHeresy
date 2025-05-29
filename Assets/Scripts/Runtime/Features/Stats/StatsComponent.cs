using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats.Modifiers;
using UniRx;

namespace EndlessHeresy.Runtime.Stats
{
    public sealed class StatsComponent : PocoComponent
    {
        private readonly IReactiveCollection<Stat> _stats;
        public IReadOnlyReactiveCollection<Stat> StatsReadOnly => _stats;

        public StatsComponent(StatData[] initialStats)
        {
            _stats = new ReactiveCollection<Stat>();
            
            foreach (var statData in initialStats)
            {
                _stats.Add(statData.GetStat());
            }
        }

        public Stat GetStat(StatType identifier)
        {
            return StatsReadOnly.FirstOrDefault(temp => temp.Identifier == identifier);
        }

        public void RemoveAllModifiersBySource(IStatModifierSource source)
        {
            foreach (var stat in _stats)
            {
                stat.RemoveAllModifiersBySource(source);
            }
        }
    }
}