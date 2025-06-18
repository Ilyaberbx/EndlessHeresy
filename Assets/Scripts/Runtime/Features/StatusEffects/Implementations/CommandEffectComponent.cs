using Better.Commons.Runtime.Extensions;
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
        private readonly IObjectResolver _resolver;
        private readonly CommandInstaller _commandInstaller;

        public CommandEffectComponent(IObjectResolver resolver, CommandInstaller commandInstaller)
        {
            _resolver = resolver;
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
                .ExecuteAsParallel(_commandInstaller.GetCommand(_resolver))
                .Forget();
        }
    }
}