using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using EndlessHeresy.Runtime.Stats;
using VContainer;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class CommandEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect
    {
        private readonly ICommandInstaller _commandInstaller;

        public CommandEffectComponent(ICommandInstaller commandInstaller)
        {
            _commandInstaller = commandInstaller;
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

            commandsInvoker.Execute(_commandInstaller.GetCommand());
        }
    }
}