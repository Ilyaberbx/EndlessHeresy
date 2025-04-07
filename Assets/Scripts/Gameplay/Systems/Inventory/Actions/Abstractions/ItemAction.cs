using System;
using EndlessHeresy.Core;

namespace EndlessHeresy.Gameplay.Inventory.Actions
{
    public abstract class ItemAction : IDisposable
    {
        private InventoryComponent _inventoryComponent;
        protected InventoryComponent Inventory => _inventoryComponent;
        public bool TryProcess(IActor owner)
        {
            return owner.TryGetComponent(out _inventoryComponent) && TryProcessInternally(owner);
        }

        protected abstract bool TryProcessInternally(IActor owner);

        public virtual void Dispose()
        {
        }
    }
}