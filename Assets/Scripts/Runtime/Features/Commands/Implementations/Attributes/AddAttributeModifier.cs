using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Attributes;
using EndlessHeresy.Runtime.Data.Static.Components;

namespace EndlessHeresy.Runtime.Commands.Attributes
{
    public sealed class AddAttributeModifier : IUndoableCommand
    {
        private readonly AttributeModifierData _data;

        public AddAttributeModifier(AttributeModifierData data)
        {
            _data = data;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var attributes = actor.GetComponent<AttributesComponent>();
            attributes.Increase(_data.Identifier, _data.Value);
            return Task.CompletedTask;
        }

        public ICommand GetUndoCommand()
        {
            return new RemoveAttributeModifier(_data);
        }
    }
}