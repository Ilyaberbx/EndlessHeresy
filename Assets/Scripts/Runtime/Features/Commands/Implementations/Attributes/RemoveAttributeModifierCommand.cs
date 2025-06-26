using EndlessHeresy.Runtime.Attributes;
using EndlessHeresy.Runtime.Data.Static.Components;

namespace EndlessHeresy.Runtime.Commands.Attributes
{
    public sealed class RemoveAttributeModifierCommand : IActorCommand, IUndoableCommand
    {
        private readonly AttributeModifierData _data;
        private IActor _actor;

        public RemoveAttributeModifierCommand(AttributeModifierData data)
        {
            _data = data;
        }

        public void Execute()
        {
            if (_actor == null)
            {
                return;
            }

            if (!_actor.TryGetComponent<AttributesComponent>(out var attributes))
            {
                return;
            }

            attributes.Decrease(_data.Identifier, _data.Value);
        }

        public ICommand GetUndoCommand()
        {
            return new AddAttributeModifierCommand(_data);
        }

        public void Setup(IActor actor)
        {
            _actor = actor;
        }
    }
}