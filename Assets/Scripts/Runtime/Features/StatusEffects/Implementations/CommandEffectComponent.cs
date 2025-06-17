using System.Threading;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class CommandEffectComponent :
        IStatusEffectComponent,
        IApplyStatusEffect
    {
        private readonly ICommand _command;

        public CommandEffectComponent(ICommand command)
        {
            _command = command;
        }

        public void Apply(StatsComponent stats)
        {
            var owner = stats.Owner;
            if (owner == null)
            {
                return;
            }

            _command
                .ExecuteAsync(owner, CancellationToken.None)
                .Forget();
        }
    }
}