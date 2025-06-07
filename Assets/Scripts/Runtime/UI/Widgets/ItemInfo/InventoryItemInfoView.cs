using EndlessHeresy.Runtime.UI.Core.MVVM;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Widgets.ItemInfo
{
    public sealed class InventoryItemInfoView : BaseView<InventoryItemInfoViewModel>
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private GameObject _infoContainer;

        protected override void Initialize(InventoryItemInfoViewModel viewModel)
        {
            ViewModel.NameProperty.Subscribe(UpdateName).AddTo(CompositeDisposable);
            ViewModel.DescriptionProperty.Subscribe(UpdateDescription).AddTo(CompositeDisposable);
            ViewModel.IconProperty.Subscribe(UpdateIcon).AddTo(CompositeDisposable);
            ViewModel.IsActiveProperty.Subscribe(UpdateActiveState).AddTo(CompositeDisposable);
        }

        private void UpdateName(string name)
        {
            _nameText.text = name;
        }

        private void UpdateDescription(string description)
        {
            _descriptionText.text = description;
        }

        private void UpdateIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
            _iconImage.gameObject.SetActive(icon != null);
        }

        private void UpdateActiveState(bool isActive)
        {
            _infoContainer.SetActive(isActive);
        }
    }
}