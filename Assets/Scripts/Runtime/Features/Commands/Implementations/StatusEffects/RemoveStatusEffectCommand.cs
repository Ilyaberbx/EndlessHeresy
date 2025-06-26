using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.StatusEffects;

namespace EndlessHeresy.Runtime.Commands.StatusEffects
{
    public sealed class RemoveStatusEffectCommand : IUndoableCommand, IActorCommand
    {
        private readonly StatusEffectType _identifier;
        private IActor _actor;

        public RemoveStatusEffectCommand(StatusEffectType identifier)
        {
            _identifier = identifier;
        }

        public void Execute()
        {
            if (_actor == null)
            {
                return;
            }

            if (!_actor.TryGetComponent<StatusEffectsComponent>(out var statusEffects))
            {
                return;
            }

            statusEffects.Remove(_identifier);
        }

        public void Setup(IActor actor)
        {
            _actor = actor;
        }

        public ICommand GetUndoCommand()
        {
            return new ApplyStatusEffectCommand(_identifier);
        }
    }
}