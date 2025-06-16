using System.Threading;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class UsableItemComponent : IItemComponent,
        IAddItemComponent,
        IRemoveItemComponent
    {
        private readonly ICommand _command;
        private IActor _owner;

        public UsableItemComponent(ICommand command)
        {
            _command = command;
        }

        public void Use()
        {
            _command.ExecuteAsync(_owner, CancellationToken.None);
        }

        public void Add(IActor actor)
        {
            _owner = actor;
        }

        public void Remove(IActor actor)
        {
            _owner = null;
        }
    }
}