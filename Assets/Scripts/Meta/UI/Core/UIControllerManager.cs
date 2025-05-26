using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlessHeresy.Meta.UI.Core.MVVM;
using EndlessHeresy.Runtime.AssetsManagement;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Meta.UI.Core
{
    public class UIControllerManager
    {
        private readonly IAssetsService _assetsService;
        private readonly IObjectResolver _resolver;
        private readonly Transform _root;
        private readonly Dictionary<BaseView, BaseViewModel> _viewMap;

        public UIControllerManager(Transform root, IAssetsService assetsService, IObjectResolver resolver)
        {
            _root = root;
            _assetsService = assetsService;
            _resolver = resolver;
            _viewMap = new Dictionary<BaseView, BaseViewModel>();
        }

        public async Task<TBaseViewModel> ShowAsync<TBaseViewModel, TModel>(TModel model, string viewPath,
            ShowType showType)
            where TBaseViewModel : BaseViewModel<TModel>, new()
            where TModel : IModel
        {
            var viewPrefab = await _assetsService.Load<BaseView>(viewPath);

            if (viewPrefab == null)
                return null;

            if (showType == ShowType.Single)
                HideAll();

            var at = _root.GetComponent<RectTransform>().position;
            var view = Object.Instantiate(viewPrefab, at, Quaternion.identity, _root);
            var viewModel = new TBaseViewModel();

            _resolver.Inject(viewModel);
            viewModel.Initialize(model);
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
                viewPair.Value.Dispose();
                Object.Destroy(viewPair.Key);
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
                Object.Destroy(viewPair.Key);
                _viewMap.Remove(viewPair.Key);
            }
        }
    }
}