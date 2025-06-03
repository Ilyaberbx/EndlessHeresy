using EndlessHeresy.Runtime.UI.Core.MVVM;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory.Item
{
    public sealed class InventoryItemView : BaseView<InventoryItemViewModel>
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _stackText;
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _equipButton;
        [SerializeField] private Button _selectionButton;
        [SerializeField] private GameObject _stackableContainer;

        protected override void Initialize(InventoryItemViewModel viewModel)
        {
            ViewModel.IconProperty.Subscribe(OnIconChanged).AddTo(CompositeDisposable);
            ViewModel.StackCountProperty.Subscribe(OnStackChanged).AddTo(CompositeDisposable);
            ViewModel.IsEquipableProperty.Subscribe(OnIsEquipableChanged).AddTo(CompositeDisposable);
            ViewModel.IsStackableProperty.Subscribe(OnIsStackableChanged).AddTo(CompositeDisposable);
            ViewModel.IsUsableProperty.Subscribe(OnIsUsableChanged).AddTo(CompositeDisposable);

            _useButton.onClick.AddListener(ViewModel.Use);
            _equipButton.onClick.AddListener(ViewModel.Equip);
            _selectionButton.onClick.AddListener(ViewModel.Select);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _useButton.onClick.RemoveListener(ViewModel.Use);
            _equipButton.onClick.RemoveListener(ViewModel.Equip);
            _selectionButton.onClick.RemoveListener(ViewModel.Select);
        }

        private void OnIconChanged(Sprite icon) => _iconImage.sprite = icon;
        private void OnStackChanged(int count) => _stackText.text = count.ToString();
        private void OnIsEquipableChanged(bool isEquipable) => _equipButton.gameObject.SetActive(isEquipable);
        private void OnIsStackableChanged(bool isStackable) => _stackableContainer.SetActive(isStackable);
        private void OnIsUsableChanged(bool isUsable) => _useButton.gameObject.SetActive(isUsable);
    }
}