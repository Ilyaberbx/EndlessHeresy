using System.Collections.Generic;
using EndlessHeresy.Runtime.Applicators;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;
using UniRx;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class EquipableItemComponent : IItemComponent
    {
        private readonly IEnumerable<IApplicator> _applicators;
        public IReactiveProperty<bool> IsEquipped { get; }

        public EquipableItemComponent(IEnumerable<IApplicator> applicators)
        {
            _applicators = applicators;
            IsEquipped = new ReactiveProperty<bool>(false);
        }

        public void Equip(MonoActor actor)
        {
            foreach (var applicator in _applicators)
            {
                applicator.Apply(actor);
            }

            IsEquipped.Value = true;
        }

        public void Unequip(MonoActor actor)
        {
            foreach (var applicator in _applicators)
            {
                applicator.Remove(actor);
            }

            IsEquipped.Value = false;
        }
    }
}