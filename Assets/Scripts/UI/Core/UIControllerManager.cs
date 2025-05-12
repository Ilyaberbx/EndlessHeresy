using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlessHeresy.Global.Services.AssetsManagement;
using EndlessHeresy.UI.MVC;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.UI.Core
{
    public class UIControllerManager
    {
        private readonly IAssetsService _assetsService;
        private readonly IObjectResolver _resolver;
        private readonly Transform _root;
        private readonly List<BaseController> _controllers = new();

        public UIControllerManager(Transform root, IAssetsService assetsService, IObjectResolver resolver)
        {
            _root = root;
            _assetsService = assetsService;
            _resolver = resolver;
        }

        public async Task<TController> ShowAsync<TController, TModel>(TModel model, string viewPath, bool additive = true)
            where TController : BaseController<TModel>, new()
            where TModel : IModel
        {
            var viewPrefab = await _assetsService.Load<BaseView>(viewPath);

            if (viewPrefab == null)
                return null;

            if (!additive)
                HideAll();

            var at = _root.GetComponent<RectTransform>().position;
            var view = Object.Instantiate(viewPrefab, at, Quaternion.identity, _root);
            var controller = new TController();

            _resolver.Inject(controller);
            controller.Initialize(view, model);
            _controllers.Add(controller);

            return controller;
        }

        public void HideAll()
        {
            if (_controllers.Count == 0)
                return;

            foreach (var controller in _controllers)
            {
                var viewGameObject = controller.DerivedView.gameObject;
                controller.Dispose();
                Object.Destroy(viewGameObject);
            }
            _controllers.Clear();
        }

        public void HideAllOfType<TController>() where TController : BaseController
        {
            var controllers = _controllers.OfType<TController>().ToList();

            if (controllers.Count == 0)
            {
                return;
            }
            
            
            foreach (var controller in controllers)
            {
                var viewGameObject = controller.DerivedView.gameObject;
                controller.Dispose();
                Object.Destroy(viewGameObject);
                _controllers.Remove(controller);
            }
        }
    }
} 