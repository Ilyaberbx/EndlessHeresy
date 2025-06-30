using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Widgets.Abilities.Item
{
    public sealed class AbilityItemView : BaseView<AbilityItemViewModel>
    {
        [SerializeField] private GameObject _inUseContainer;
        [SerializeField] private GameObject _readyContainer;
        [SerializeField] private GameObject _cooldownContainer;
        [SerializeField] private Image _cooldownProgressImage;
        [SerializeField] private Image _iconImage;

        protected override void Initialize(AbilityItemViewModel viewModel)
        {
            ViewModel.StateProperty
                .Subscribe(OnStateChanged)
                .AddTo(CompositeDisposable);

            ViewModel.CooldownProgress
                .Subscribe(OnCooldownProgressChanged)
                .AddTo(CompositeDisposable);

            ViewModel.IconProperty
                .Subscribe(OnIconChanged)
                .AddTo(CompositeDisposable);
        }


        private void OnCooldownProgressChanged(float progress)
        {
            _cooldownProgressImage.fillAmount = progress;
        }

        private void OnIconChanged(Sprite icon) => _iconImage.sprite = icon;

        private void OnStateChanged(AbilityState state)
        {
            _readyContainer.SetActive(state == AbilityState.Ready);
            _inUseContainer.SetActive(state == AbilityState.InUse);
            _cooldownContainer.SetActive(state == AbilityState.Cooldown);
        }
    }
}