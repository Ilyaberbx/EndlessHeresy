using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.UI.Widgets.Slot;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory.Item
{
    public sealed class InventoryItemView : ItemCoreView<InventoryItemViewModel>
    {
        [SerializeField] private TextMeshProUGUI _stackText;
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _equipButton;
        [SerializeField] private GameObject _stackableContainer;

        protected override void Initialize(InventoryItemViewModel viewModel)
        {
            ViewModel.IconProperty.Subscribe(OnIconChanged).AddTo(CompositeDisposable);
            ViewModel.StackCountProperty.Subscribe(OnStackChanged).AddTo(CompositeDisposable);
            ViewModel.IsEquipableProperty.Subscribe(OnIsEquipableChanged).AddTo(CompositeDisposable);
            ViewModel.IsStackableProperty.Subscribe(OnIsStackableChanged).AddTo(CompositeDisposable);
            ViewModel.IsUsableProperty.Subscribe(OnIsUsableChanged).AddTo(CompositeDisposable);
            ViewModel.IsSelectedProperty.Subscribe(OnIsSelectedChanged).AddTo(CompositeDisposable);

            Observable
                .CombineLatest(ViewModel.IsEquipableProperty, ViewModel.IsUsableProperty)
                .Subscribe(OnSelectionStateChanged)
                .AddTo(CompositeDisposable);

            _useButton.onClick.AddListener(ViewModel.Use);
            _equipButton.onClick.AddListener(ViewModel.Equip);
            SelectionButton.onClick.AddListener(ViewModel.Select);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _useButton.onClick.RemoveListener(ViewModel.Use);
            _equipButton.onClick.RemoveListener(ViewModel.Equip);
            SelectionButton.onClick.RemoveListener(ViewModel.Select);
        }

        private void OnIconChanged(Sprite icon) => IconImage.sprite = icon;
        private void OnStackChanged(int count) => _stackText.text = count.ToString();
        private void OnIsEquipableChanged(bool isEquipable) => _equipButton.gameObject.SetActive(isEquipable);
        private void OnIsStackableChanged(bool isStackable) => _stackableContainer.SetActive(isStackable);
        private void OnIsUsableChanged(bool isUsable) => _useButton.gameObject.SetActive(isUsable);
        private void OnIsSelectedChanged(bool isSelected) => SelectionContainer.SetActive(isSelected);

        private void OnSelectionStateChanged(IList<bool> actions)
        {
            SelectionButton.gameObject.SetActive(actions.Any());
        }
    }
}