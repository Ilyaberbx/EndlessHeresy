using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Services.AssetsManagement;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Core
{
    public class UILayerManager
    {
        private readonly IAssetsService _assetsService;
        private readonly Transform _root;
        private readonly Dictionary<BaseView, BaseViewModel> _viewMap;

        private IViewModelFactory _factory;

        public UILayerManager(Transform root, IAssetsService assetsService)
        {
            _root = root;
            _assetsService = assetsService;
            _viewMap = new Dictionary<BaseView, BaseViewModel>();
        }

        public void UpdateFactory(IViewModelFactory factory)
        {
            _factory = factory;
        }
        
        public async Task<TViewModel> ShowAsync<TViewModel, TModel>(TModel model, string viewPath,
            ShowType showType)
            where TViewModel : BaseViewModel<TModel>
            where TModel : IModel
        {
            var viewPrefab = await _assetsService.Load<BaseView>(viewPath);

            if (viewPrefab == null)
                return null;

            if (showType == ShowType.Single)
                HideAll();

            var at = _root.GetComponent<RectTransform>().position;
            var view = Object.Instantiate(viewPrefab, at, Quaternion.identity, _root);
            var viewModel = _factory.Create<TViewModel, TModel>(model);
            view.Initialize(viewModel);
            _viewMap.Add(view, viewModel);
            return viewModel;
        }

        public void HideAll()
        {
            if (_viewMap.Count == 0)
                return;

            foreach (var viewPair in _viewMap)
            {
                Object.Destroy(viewPair.Key.gameObject);
                viewPair.Value.Dispose();
            }

            _viewMap.Clear();
        }

        public void HideAllOfType<TViewModel>() where TViewModel : BaseViewModel
        {
            var viewPairs = _viewMap.Where(pair => pair.Value is TViewModel).ToArray();

            if (viewPairs.Length == 0)
            {
                return;
            }

            foreach (var viewPair in viewPairs.ToArray())
            {
                viewPair.Value.Dispose();
                Object.Destroy(viewPair.Key.gameObject);
                _viewMap.Remove(viewPair.Key);
            }
        }
    }
}