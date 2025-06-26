using EndlessHeresy.Runtime.Attributes;
using EndlessHeresy.Runtime.Data.Static.Components;

namespace EndlessHeresy.Runtime.Commands.Attributes
{
    public sealed class AddAttributeModifierCommand : IUndoableCommand, IActorCommand
    {
        private readonly AttributeModifierData _data;
        private IActor _actor;

        public AddAttributeModifierCommand(AttributeModifierData data)
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

            attributes.Increase(_data.Identifier, _data.Value);
        }

        public ICommand GetUndoCommand()
        {
            return new RemoveAttributeModifierCommand(_data);
        }

        public void Setup(IActor actor)
        {
            _actor = actor;
        }
    }
}