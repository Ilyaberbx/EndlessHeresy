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
        
        protected override void Initialize(AttributeItemViewModel viewModel)
        {
            ViewModel.IconProperty.Subscribe(OnIconChanged).AddTo(CompositeDisposable);
            ViewModel.ValueProperty.Subscribe(OnValueChanged).AddTo(CompositeDisposable);
        }

        private void OnIconChanged(Sprite icon) => _iconImage.sprite = icon;
        private void OnValueChanged(int value) => _valueText.text = value.ToString();
    }
}