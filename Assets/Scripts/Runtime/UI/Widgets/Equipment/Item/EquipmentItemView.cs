using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.ItemSelection;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Widgets.Equipment.Item
{
    public sealed class EquipmentItemView : ItemCoreView<EquipmentItemViewModel>
    {
        [SerializeField] private Button _unequipButton;

        protected override void Initialize(EquipmentItemViewModel viewModel)
        {
            ViewModel.IconProperty.Subscribe(OnIconChanged).AddTo(CompositeDisposable);
            ViewModel.IsSelectedProperty.Subscribe(OnIsSelectedChanged).AddTo(CompositeDisposable);

            _unequipButton.onClick.AddListener(ViewModel.Unequip);
            SelectionButton.onClick.AddListener(ViewModel.Select);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _unequipButton.onClick.RemoveListener(ViewModel.Unequip);
            SelectionButton.onClick.RemoveListener(ViewModel.Select);
        }

        private void OnIconChanged(Sprite icon) => IconImage.sprite = icon;
        private void OnIsSelectedChanged(bool isSelected) => SelectionContainer.SetActive(isSelected);
    }
}