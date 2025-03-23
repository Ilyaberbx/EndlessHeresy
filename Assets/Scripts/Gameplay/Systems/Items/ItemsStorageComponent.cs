using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Items.Abstractions;
using EndlessHeresy.Gameplay.Services.StaticData;
using VContainer;

namespace EndlessHeresy.Gameplay.Items
{
    public class ItemsStorageComponent : PocoComponent
    {
        private readonly Locator<ItemType, ItemController> _locator = new();
        private IGameplayStaticDataService _gameplayStaticDataService;

        [Inject]
        public void Construct(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        public void AddItem(ItemType itemType)
        {
            var configuration = _gameplayStaticDataService.GetItemConfiguration(itemType);

            if (_locator.ContainsKey(itemType))
            {
                return;
            }
            
            var item = _locator.Get(itemType);
        }
    }
}