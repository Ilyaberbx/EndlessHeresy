using System.Threading.Tasks;
using EndlessHeresy.Runtime.Services.AssetsManagement;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Services.Modals
{
    public sealed class ModalsService : IModalsService
    {
        private const string ViewModelPostfix = "ViewModel";
        private const string ViewPathFormat = "Modals/{0}";

        private readonly UIControllerManager _manager;

        public ModalsService(Transform root, IAssetsService assetsService)
        {
            _manager = new UIControllerManager(root, assetsService);
        }

        public Task<TViewModel> ShowAsync<TViewModel, TModel>(TModel model)
            where TViewModel : BaseViewModel<TModel>
            where TModel : IModel
        {
            var viewModelName = typeof(TViewModel).Name;
            var viewKey = viewModelName.Replace(ViewModelPostfix, string.Empty);
            var address = string.Format(ViewPathFormat, viewKey);
            return _manager.ShowAsync<TViewModel, TModel>(model, address, ShowType.Single);
        }

        public void UpdateFactory(IViewModelFactory factory)
        {
            _manager.UpdateFactory(factory);
        }

        public void HideAll()
        {
            _manager.HideAll();
        }
    }
}