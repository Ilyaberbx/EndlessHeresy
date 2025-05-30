using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Widgets.Common
{
    public sealed class CloseModalWindowView : BaseView<CloseModalWindowViewModel>
    {
        [SerializeField] private Button _closeButton;
        private void OnEnable() => _closeButton.onClick.AddListener(OnCloseButtonClicked);
        private void OnDisable() => _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        private void OnCloseButtonClicked() => ViewModel.OnCloseClicked();
    }
}