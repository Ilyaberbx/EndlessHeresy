using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Commands.Abilities
{
    public sealed class CastAbility : ICommand
    {
        private readonly AbilityType _abilityIdentifier;

        public CastAbility(AbilityType abilityIdentifier)
        {
            _abilityIdentifier = abilityIdentifier;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            actor.GetComponent<AbilitiesCastComponent>();
            return Task.CompletedTask;
        }
    }
}