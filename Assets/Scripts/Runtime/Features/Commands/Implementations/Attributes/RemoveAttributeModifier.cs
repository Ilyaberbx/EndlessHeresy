using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Attributes;
using EndlessHeresy.Runtime.Data.Static.Components;

namespace EndlessHeresy.Runtime.Commands.Attributes
{
    public sealed class RemoveAttributeModifier : IUndoableCommand
    {
        private readonly AttributeModifierData _data;

        public RemoveAttributeModifier(AttributeModifierData data)
        {
            _data = data;
        }

        public Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var attributes = actor.GetComponent<AttributesComponent>();
            attributes.Decrease(_data.Identifier, _data.Value);
            return Task.CompletedTask;
        }

        public ICommand GetUndoCommand()
        {
            return new AddAttributeModifier(_data);
        }
    }
}