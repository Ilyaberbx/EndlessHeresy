using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;

namespace EndlessHeresy.Runtime.Commands.Stats
{
    public sealed class AddStatModifier : IStatModifierSource, IUndoableCommand
    {
        private readonly StatModifierData _data;

        public AddStatModifier(StatModifierData data)
        {
            _data = data;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var stats = actor.GetComponent<StatsComponent>();
            stats.GetStat(_data.StatIdentifier).AddModifier(_data.GetStatModifier(this));
            return Task.CompletedTask;
        }

        public ICommand GetUndoCommand()
        {
            return new RemoveStatModifier(this, _data);
        }
    }
}