using System;
using System.Linq;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Equipment.Item
{
    public sealed class EquipmentItemViewModel : BaseViewModel<EquipmentItemModel>
    {
        public event Action<ItemRoot> OnSelected;
        private readonly IGameplayStaticDataService _gameplayStaticDataService;

        public IReactiveProperty<Sprite> IconProperty { get; }
        public IReactiveProperty<bool> IsSelectedProperty { get; }

        public EquipmentItemViewModel(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            IconProperty = new ReactiveProperty<Sprite>();
            IsSelectedProperty = new ReactiveProperty<bool>();
        }

        protected override void Initialize(EquipmentItemModel model)
        {
            UpdateIcon();
            Deselect();
        }

        public void Unequip()
        {
            var equipableComponent = Model.Item.Components.OfType<EquipableItemComponent>().FirstOrDefault();
            equipableComponent?.Unequip(Model.Owner, EquipmentSlotType.Attack);
        }

        public void Select()
        {
            IsSelectedProperty.Value = true;
            OnSelected?.Invoke(Model.Item);
        }

        public void Deselect()
        {
            IsSelectedProperty.Value = false;
        }

        private void UpdateIcon()
        {
            var data = _gameplayStaticDataService.GetItemData(Model.Item.Identifier);
            IconProperty.Value = data.Icon;
        }
    }
}