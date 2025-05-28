using EndlessHeresy.Runtime.UI.Core.MVVM;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Widgets.StatusEffects.Item
{
    public sealed class StatusEffectItemView : BaseView<StatusEffectItemViewModel>
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private GameObject _temporaryContainer;
        [SerializeField] private GameObject _stackContainer;
        [SerializeField] private Image _progressImage;
        [SerializeField] private TextMeshProUGUI _stackText;

        protected override void Initialize(StatusEffectItemViewModel viewModel)
        {
            viewModel.IconProperty.Subscribe(OnIconChanged).AddTo(CompositeDisposable);
            viewModel.TemporaryProgressProperty.Subscribe(OnProgressChanged).AddTo(CompositeDisposable);
            viewModel.IsTemporaryEnabledProperty.Subscribe(OnIsTemporaryEnabledChanged).AddTo(CompositeDisposable);
            viewModel.IsStackableEnabledProperty.Subscribe(OnIsStackableEnabledChanged).AddTo(CompositeDisposable);
            viewModel.StackProperty.Subscribe(OnStackChanged).AddTo(CompositeDisposable);
        }

        private void OnIconChanged(Sprite icon) => _iconImage.sprite = icon;
        private void OnProgressChanged(float progress) => _progressImage.fillAmount = progress;
        private void OnIsTemporaryEnabledChanged(bool isEnabled) => _temporaryContainer.SetActive(isEnabled);
        private void OnIsStackableEnabledChanged(bool isEnabled) => _stackContainer.SetActive(isEnabled);
        private void OnStackChanged(int stack) => _stackText.text = stack.ToString();
    }
}