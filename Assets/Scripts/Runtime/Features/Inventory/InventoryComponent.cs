using System.Linq;
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

        public InventoryComponent(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            _items = new ReactiveCollection<ItemRoot>();
        }

        public void Add(ItemType identifier)
        {
            var existingItem = _items.FirstOrDefault(item => item.Identifier.Equals(identifier));
            var stackableComponent = existingItem?.Components
                .OfType<StackableItemComponent>()
                .FirstOrDefault();

            if (stackableComponent != null)
            {
                stackableComponent.AddStack();
                return;
            }

            var itemData = _gameplayStaticDataService.GetItemData(identifier);
            if (itemData == null)
            {
                UnityEngine.Debug.LogError($"Item data not found for identifier: {identifier}");
                return;
            }

            var newItem = itemData.GetInstance();
            _items.Add(newItem);
        }

        public void Remove(ItemType identifier)
        {
            var itemToRemove = _items.FirstOrDefault(item => item.Identifier.Equals(identifier));
            if (itemToRemove == null)
            {
                UnityEngine.Debug.LogWarning($"Item with identifier {identifier} not found in inventory.");
                return;
            }

            _items.Remove(itemToRemove);
        }
    }
}