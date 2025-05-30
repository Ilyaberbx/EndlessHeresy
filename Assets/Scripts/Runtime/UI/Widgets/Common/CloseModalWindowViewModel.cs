using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Services.Modals;

namespace EndlessHeresy.Runtime.UI.Widgets.Common
{
    public sealed class CloseModalWindowViewModel : BaseViewModel
    {
        private readonly IModalsService _modalsService;

        public CloseModalWindowViewModel(IModalsService modalsService)
        {
            _modalsService = modalsService;
        }

        public void OnCloseClicked()
        {
            _modalsService.HideAll();
        }
    }
}