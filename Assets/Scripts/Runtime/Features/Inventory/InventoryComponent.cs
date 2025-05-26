using System.Collections.Generic;
using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Scopes.Gameplay.Services.StaticData;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Inventory
{
    public class InventoryComponent : PocoComponent
    {
        private IGameplayStaticDataService _gameplayStaticDataService;
        private readonly List<ItemType> _items = new();
        private int _maxSize;
        private int _currentSize;

        [Inject]
        public void Construct(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        public void SetMaxSize(int size) => _maxSize = size;

        public void Add(ItemType itemType)
        {
            if (IsReachedMaxSize())
            {
                return;
            }

            var configuration = _gameplayStaticDataService.GetItemConfiguration(itemType);
            var storeAction = configuration.GetOnStoreAction();
            var isStored = storeAction.TryProcess(Owner);

            if (!isStored)
            {
                Debug.Log($"@Cannot process store item: {itemType} ---> {storeAction}");
                return;
            }

            _items.Add(itemType);
            _currentSize++;
        }

        public bool Remove(ItemType itemType)
        {
            if (!_items.Remove(itemType))
            {
                return false;
            }

            var configuration = _gameplayStaticDataService.GetItemConfiguration(itemType);
            var removeAction = configuration.GetOnRemoveAction();

            if (removeAction.TryProcess(Owner))
            {
                _currentSize--;
                return true;
            }

            Debug.Log($"@Cannot process removed item: {itemType} ---> {removeAction}");
            return false;
        }

        private bool IsReachedMaxSize() => _currentSize >= _maxSize;
    }
}