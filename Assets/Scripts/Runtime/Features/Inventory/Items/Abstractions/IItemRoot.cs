using System.Collections.Generic;

namespace EndlessHeresy.Runtime.Inventory.Items.Abstractions
{
    public interface IItemRoot
    {
        public IReadOnlyCollection<IItemComponent> Components { get; }
    }
}