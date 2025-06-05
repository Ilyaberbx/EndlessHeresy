using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Inventory.Items.Abstractions;

namespace EndlessHeresy.Runtime.Inventory.Items.Implementations
{
    public sealed class ItemRoot : IItemComponent, IAddItemComponent, IRemoveItemComponent
    {
        public ItemType Identifier { get; }
        public IEnumerable<IItemComponent> Components { get; }
        public int Index { get; }

        public ItemRoot(ItemType identifier, IItemComponent[] components, int index)
        {
            Identifier = identifier;
            Components = components;
            Index = index;
        }

        public void Add(IActor actor)
        {
            foreach (var component in Components)
            {
                if (component is IAddItemComponent addComponent)
                {
                    addComponent.Add(actor);
                }

                if (component is IRootHandlerItemComponent rootHandlerComponent)
                {
                    rootHandlerComponent.Initialize(this);
                }
            }
        }

        public void Remove(IActor actor)
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