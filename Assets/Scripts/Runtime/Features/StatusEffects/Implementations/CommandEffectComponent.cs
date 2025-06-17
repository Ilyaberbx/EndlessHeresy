using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class CommandEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect
    {
        private readonly CommandInstaller _commandInstaller;

        public CommandEffectComponent(CommandInstaller commandInstaller)
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

            if (!owner.TryGetComponent<CommandsComponent>(out var commands))
            {
                return;
            }

            commands
                .ExecuteAsParallel(_commandInstaller.GetCommand())
                .Forget();
        }
    }
}