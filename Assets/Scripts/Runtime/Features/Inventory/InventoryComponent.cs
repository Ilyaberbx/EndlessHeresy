using System;
using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using UniRx;

namespace EndlessHeresy.Runtime.Inventory
{
    public sealed class InventoryComponent : PocoComponent
    {
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        private readonly ReactiveCollection<ItemRoot> _items;
        public IReadOnlyReactiveCollection<ItemRoot> Items => _items;
        public int MaxSize { get; }

        public InventoryComponent(IGameplayStaticDataService gameplayStaticDataService, int maxSize)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            MaxSize = maxSize;
            _items = new ReactiveCollection<ItemRoot>();
        }

        public void Add(ItemType identifier)
        {
            var existingItems = _items.Where(item => item.Identifier.Equals(identifier)).ToArray();

            if (!existingItems.IsNullOrEmpty())
            {
                if (existingItems.SelectMany(StackableItemsSelector())
                    .Where(stackableComponent => stackableComponent != null)
                    .Any(stackableComponent => stackableComponent.AddStack()))
                {
                    return;
                }
            }

            if (_items.Count >= MaxSize)
            {
                return;
            }

            var itemData = _gameplayStaticDataService.GetItemData(identifier);
            if (itemData == null)
            {
                UnityEngine.Debug.LogError($"Item data not found for identifier: {identifier}");
                return;
            }

            var index = existingItems.Length;
            var newItem = itemData.GetInstance(index);
            newItem.Add(Owner);
            _items.Add(newItem);
        }

        public void Remove(ItemType identifier, int index)
        {
            var itemToRemove = _items.FirstOrDefault(item => item.Identifier.Equals(identifier) && item.Index == index);
            if (itemToRemove == null)
            {
                UnityEngine.Debug.LogWarning(
                    $"Item with Identifier {identifier} and Index {index} not found in inventory.");
                return;
            }

            var stackableComponent = itemToRemove.Components
                .OfType<StackableItemComponent>()
                .FirstOrDefault();

            if (stackableComponent != null)
            {
                if (stackableComponent.RemoveStack())
                {
                    return;
                }
            }

            itemToRemove.Remove(Owner);
            _items.Remove(itemToRemove);
        }

        private static Func<ItemRoot, IEnumerable<StackableItemComponent>> StackableItemsSelector()
        {
            return existingItem => existingItem
                .Components
                .OfType<StackableItemComponent>();
        }
    }
}