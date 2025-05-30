using EndlessHeresy.Runtime.UI.Core.MVVM;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Widgets.Attributes.Item
{
    public sealed class AttributeItemView : BaseView<AttributeItemViewModel>
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private TextMeshProUGUI _nameText;

        protected override void Initialize(AttributeItemViewModel viewModel)
        {
            ViewModel.IconProperty.Subscribe(OnIconChanged).AddTo(CompositeDisposable);
            ViewModel.ValueProperty.Subscribe(OnValueChanged).AddTo(CompositeDisposable);
            ViewModel.NameProperty.Subscribe(OnNameChanged).AddTo(CompositeDisposable);
        }

        private void OnIconChanged(Sprite icon) => _iconImage.sprite = icon;
        private void OnValueChanged(int value) => _valueText.text = value.ToString();
        private void OnNameChanged(string name) => _nameText.text = name;
    }
}