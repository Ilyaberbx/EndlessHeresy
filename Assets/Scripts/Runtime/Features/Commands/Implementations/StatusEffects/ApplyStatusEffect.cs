using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.StatusEffects;

namespace EndlessHeresy.Runtime.Commands.StatusEffects
{
    public sealed class ApplyStatusEffect : IUndoableCommand
    {
        private readonly StatusEffectType _identifier;

        public ApplyStatusEffect(StatusEffectType identifier)
        {
            _identifier = identifier;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var statusEffects = actor.GetComponent<StatusEffectsComponent>();
            statusEffects.Add(_identifier);
            return Task.CompletedTask;
        }

        public ICommand GetUndoCommand()
        {
            return new RemoveStatusEffect(_identifier);
        }
    }
}