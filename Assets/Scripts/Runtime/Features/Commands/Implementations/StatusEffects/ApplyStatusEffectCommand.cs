using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.StatusEffects;

namespace EndlessHeresy.Runtime.Commands.StatusEffects
{
    public sealed class ApplyStatusEffectCommand : IUndoableCommand, IActorCommand
    {
        private readonly StatusEffectType _identifier;
        private IActor _actor;

        public ApplyStatusEffectCommand(StatusEffectType identifier)
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

            statusEffects.Add(_identifier);
        }

        public ICommand GetUndoCommand()
        {
            return new RemoveStatusEffectCommand(_identifier);
        }

        public void Setup(IActor actor)
        {
            _actor = actor;
        }
    }
}