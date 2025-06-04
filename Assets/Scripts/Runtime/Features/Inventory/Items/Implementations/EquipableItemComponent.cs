using System.Collections.Generic;
using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class EquipableItemComponent : IItemComponent
    {
        private readonly IEnumerable<IApplicator> _applicators;

        public EquipableItemComponent(IEnumerable<IApplicator> applicators)
        {
            _applicators = applicators;
        }

        public void Equip(MonoActor actor)
        {
            foreach (var applicator in _applicators)
            {
                applicator.Apply(actor);
            }
        }

        public void Unequip(MonoActor actor)
        {
            foreach (var applicator in _applicators)
            {
                applicator.Remove(actor);
            }
        }
    }
}