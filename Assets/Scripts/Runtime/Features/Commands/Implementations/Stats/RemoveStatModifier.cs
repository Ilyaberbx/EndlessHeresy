using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;

namespace EndlessHeresy.Runtime.Commands.Stats
{
    public sealed class RemoveStatModifier : ICommand, IUndoableCommand
    {
        private readonly IStatModifierSource _source;
        private readonly StatModifierData _data;

        public RemoveStatModifier(IStatModifierSource source, StatModifierData data)
        {
            _source = source;
            _data = data;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var stats = actor.GetComponent<StatsComponent>();
            stats.GetStat(_data.StatIdentifier).RemoveAllModifiersBySource(_source);
            return Task.CompletedTask;
        }

        public ICommand GetUndoCommand()
        {
            return new AddStatModifier(_data);
        }
    }
}