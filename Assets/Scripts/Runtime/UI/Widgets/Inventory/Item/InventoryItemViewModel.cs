using System;
using System.Linq;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory.Item
{
    public sealed class InventoryItemViewModel : BaseViewModel<InventoryItemModel>
    {
        public event Action<ItemRoot> OnSelected;
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        public IReactiveProperty<Sprite> IconProperty { get; }
        public IReactiveProperty<int> StackCountProperty { get; }
        public IReactiveProperty<bool> IsStackableProperty { get; }
        public IReactiveProperty<bool> IsSelectedProperty { get; }
        public IReactiveProperty<bool> IsEquipableProperty { get; }
        public IReactiveProperty<bool> IsUsableProperty { get; }

        public InventoryItemViewModel(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            IconProperty = new ReactiveProperty<Sprite>();
            StackCountProperty = new ReactiveProperty<int>();
            IsStackableProperty = new ReactiveProperty<bool>();
            IsSelectedProperty = new ReactiveProperty<bool>();
            IsEquipableProperty = new ReactiveProperty<bool>();
            IsUsableProperty = new ReactiveProperty<bool>();
        }

        protected override void Initialize(InventoryItemModel model)
        {
            UpdateIcon();
            UpdateStackable();
            UpdateEquipable();
            UpdateUsable();
            Deselect();
        }

        public void Use()
        {
            var usableComponent = Model.Item.Components.OfType<UsableItemComponent>().FirstOrDefault();
            usableComponent?.Use();
            Model.Inventory.Remove(Model.Item.Identifier, Model.Item.Index);
            Deselect();
        }

        public void Equip()
        {
            var equipableComponent = Model.Item.Components.OfType<EquipableItemComponent>().FirstOrDefault();

            if (equipableComponent == null)
            {
                return;
            }

            if (equipableComponent.TryEquip(Model.Owner, EquipmentSlotType.Attack))
            {
                Deselect();
            }
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

        private void UpdateEquipable()
        {
            IsEquipableProperty.Value = Model
                .Item
                .Components
                .OfType<EquipableItemComponent>()
                .Any();
        }

        private void UpdateUsable()
        {
            IsUsableProperty.Value = Model
                .Item
                .Components
                .OfType<UsableItemComponent>()
                .Any();
        }

        private void UpdateIcon()
        {
            var data = _gameplayStaticDataService.GetItemData(Model.Item.Identifier);
            IconProperty.Value = data.Icon;
        }

        private void UpdateStackable()
        {
            var stackableComponent = Model.Item.Components.OfType<StackableItemComponent>().FirstOrDefault();

            IsStackableProperty.Value = stackableComponent != null;

            if (stackableComponent == null)
            {
                StackCountProperty.Value = 1;
                return;
            }

            stackableComponent.StackCountProperty
                .Subscribe(OnStackValueChanged)
                .AddTo(CompositeDisposable);
        }

        private void OnStackValueChanged(int stack) => StackCountProperty.Value = stack;
    }
}