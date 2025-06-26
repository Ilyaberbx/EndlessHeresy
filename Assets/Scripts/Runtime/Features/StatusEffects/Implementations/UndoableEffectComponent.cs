using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class UndoableEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect
    {
        private readonly IUndoableCommand[] _undoableCommands;

        public UndoableEffectComponent(IUndoableCommand[] undoableCommands)
        {
            _undoableCommands = undoableCommands;
        }

        public void Apply(StatsComponent stats)
        {
            var owner = stats.Owner;
            if (owner == null)
            {
                return;
            }

            if (!owner.TryGetComponent<CommandsInvokerComponent>(out var commandsInvoker))
            {
                return;
            }

            foreach (var command in _undoableCommands)
            {
                commandsInvoker.Execute(command);
            }
        }

        public void Remove(StatsComponent stats)
        {
            var owner = stats.Owner;
            if (owner == null)
            {
                return;
            }

            if (!owner.TryGetComponent<CommandsInvokerComponent>(out var commandsInvoker))
            {
                return;
            }

            foreach (var command in _undoableCommands)
            {
                commandsInvoker.Execute(command.GetUndoCommand());
            }
        }
    }
}