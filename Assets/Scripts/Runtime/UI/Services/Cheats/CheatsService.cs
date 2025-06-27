using EndlessHeresy.Runtime.Services.AssetsManagement;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Core.Factory;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Services.Cheats
{
    public sealed class CheatsService : ICheatsService
    {
        private readonly UILayerManager _manager;

        public CheatsService(Transform root, IAssetsService assetsService)
        {
            _manager = new UILayerManager(root, assetsService);
        }

        public void Show()
        {
#if DEVELOPMENT_BUILD
            _manager.Show<CheatsHudViewModel>();
#endif
        }

        public void Hide()
        {
#if DEVELOPMENT_BUILD
            _manager.HideAll();
#endif
        }

        public void UpdateFactory(IViewModelFactory factory)
        {
            _manager.UpdateFactory(factory);
        }
    }
}