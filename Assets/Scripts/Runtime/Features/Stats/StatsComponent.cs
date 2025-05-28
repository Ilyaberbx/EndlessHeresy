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
        private readonly StatData[] _initialStats;
        private readonly IReactiveCollection<Stat> _stats;
        public IReadOnlyReactiveCollection<Stat> StatsReadOnly => _stats;

        public StatsComponent(StatData[] initialStats)
        {
            _initialStats = initialStats;
            _stats = new ReactiveCollection<Stat>();
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            foreach (var statData in _initialStats)
            {
                _stats.Add(statData.GetStat());
            }

            return Task.CompletedTask;
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