using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class CommandEffectComponent : IStatusEffectComponent, IApplyStatusEffect
    {
        private readonly ICommand _command;

        public CommandEffectComponent(ICommand command)
        {
            _command = command;
        }

        public void Apply(StatsComponent stats)
        {
            var owner = stats.Owner;
            if (!owner.TryGetComponent<CommandsInvokerComponent>(out var commandsInvoker))
            {
                return;
            }

            commandsInvoker.Execute(_command);
        }
    }
}