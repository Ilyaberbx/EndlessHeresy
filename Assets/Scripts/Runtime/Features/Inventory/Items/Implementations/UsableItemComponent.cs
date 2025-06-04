using System.Collections.Generic;
using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class UsableItemComponent : IItemComponent,
        IAddItemComponent,
        IRemoveItemComponent,
        IRootHandlerItemComponent
    {
        private readonly IEnumerable<IApplicator> _applicators;
        private IActor _owner;
        private ItemRoot _root;

        public UsableItemComponent(IEnumerable<IApplicator> applicators)
        {
            _applicators = applicators;
        }

        public void Use()
        {
            foreach (var applicator in _applicators)
            {
                applicator.Apply(_owner);
            }

            _owner.GetComponent<InventoryComponent>().Remove(_root.Identifier);
        }

        public void Add(IActor actor)
        {
            _owner = actor;
        }

        public void Remove(IActor actor)
        {
            _owner = null;
        }

        public void Initialize(ItemRoot root)
        {
            _root = root;
        }
    }
}