using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class UsableItemComponent : IItemComponent, IAddItemComponent, IRemoveItemComponent
    {
        private readonly IApplicator[] _applicators;
        private MonoActor _owner;

        public UsableItemComponent(IApplicator[] applicators)
        {
            _applicators = applicators;
        }

        public void Use()
        {
            foreach (var applicator in _applicators)
            {
                applicator.Apply(_owner);
            }
        }

        public void Add(MonoActor actor)
        {
            _owner = actor;
        }

        public void Remove(MonoActor actor)
        {
            _owner = null;
        }
    }
}