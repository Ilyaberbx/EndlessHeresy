using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class ItemRoot : IItemComponent, IAddItemComponent, IRemoveItemComponent
    {
        public ItemType Identifier { get; }
        public IEnumerable<IItemComponent> Components { get; }

        public ItemRoot(ItemType identifier, IEnumerable<IItemComponent> components)
        {
            Identifier = identifier;
            Components = components;
        }

        public void Add(MonoActor actor)
        {
            foreach (var component in Components)
            {
                if (component is IAddItemComponent addComponent)
                {
                    addComponent.Add(actor);
                }
            }
        }

        public void Remove(MonoActor actor)
        {
            foreach (var component in Components)
            {
                if (component is IRemoveItemComponent removeComponent)
                {
                    removeComponent.Remove(actor);
                }
            }
        }
    }
}