using System;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Services.AssetsManagement;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Huds.Cheats;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Services.Cheats
{
    public sealed class CheatsService : ICheatsService
    {
        private const string ViewModelPostfix = "ViewModel";
        private const string ViewPathFormat = "Huds/{0}";

        private readonly UILayerManager _manager;

        public CheatsService(Transform root, IAssetsService assetsService)
        {
            _manager = new UILayerManager(root, assetsService);
        }

        public void Show(CheatsHudModel model)
        {
            const string viewModelName = nameof(CheatsHudViewModel);
            var viewKey = viewModelName.Replace(ViewModelPostfix, string.Empty);
            var address = string.Format(ViewPathFormat, viewKey);
            _manager.ShowAsync<CheatsHudViewModel, CheatsHudModel>(model, address, ShowType.Single).Forget();
        }

        public void Hide()
        {
            _manager.HideAll();
        }

        public void UpdateFactory(IViewModelFactory factory)
        {
            _manager.UpdateFactory(factory);
        }
    }
}