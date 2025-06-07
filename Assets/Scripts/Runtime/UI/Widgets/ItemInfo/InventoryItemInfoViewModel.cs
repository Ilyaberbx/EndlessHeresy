using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.ItemInfo
{
    public sealed class InventoryItemInfoViewModel : BaseViewModel
    {
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        public IReactiveProperty<string> NameProperty { get; }
        public IReactiveProperty<string> DescriptionProperty { get; }
        public IReactiveProperty<Sprite> IconProperty { get; }
        public IReactiveProperty<bool> IsActiveProperty { get; }

        public InventoryItemInfoViewModel(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            NameProperty = new ReactiveProperty<string>();
            DescriptionProperty = new ReactiveProperty<string>();
            IconProperty = new ReactiveProperty<Sprite>();
            IsActiveProperty = new ReactiveProperty<bool>();
        }

        public void Select(ItemRoot item)
        {
            var data = _gameplayStaticDataService.GetItemData(item.Identifier);
            NameProperty.Value = data.Name;
            DescriptionProperty.Value = data.Description;
            IconProperty.Value = data.Icon;
            IsActiveProperty.Value = true;
        }

        public void Deselect()
        {
            NameProperty.Value = string.Empty;
            DescriptionProperty.Value = string.Empty;
            IconProperty.Value = null;
            IsActiveProperty.Value = false;
        }
    }
}