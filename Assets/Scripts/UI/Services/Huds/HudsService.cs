using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Global.Services.AssetsManagement;
using EndlessHeresy.UI.MVC;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.UI.Services.Huds
{
    public sealed class HudsService : IHudsService
    {
        private const string ControllerPostfix = "Controller";
        private const string ViewPathFormat = "Huds/{0}";

        private readonly IAssetsService _assetsService;
        private readonly IObjectResolver _resolver;
        private readonly List<BaseController> _controllers = new();
        private readonly Transform _root;

        public HudsService(Transform root, IAssetsService assetsService, IObjectResolver resolver)
        {
            _root = root;
            _assetsService = assetsService;
            _resolver = resolver;
        }

        public async Task<TController> ShowAsync<TController, TModel>(TModel model, ShowType showType)
            where TController : BaseController<TModel>, new()
            where TModel : IModel
        {
            var controllerName = typeof(TController).Name;
            var viewKey = controllerName.Replace(ControllerPostfix, string.Empty);
            var address = string.Format(ViewPathFormat, viewKey);
            var viewPrefab = await _assetsService.Load<BaseView>(address);

            if (viewPrefab == null)
            {
                return null;
            }

            if (showType == ShowType.Single)
            {
                HideAll();
            }

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
            if (_controllers.IsNullOrEmpty())
            {
                return;
            }

            foreach (var controller in _controllers)
            {
                var viewGameObject = controller.DerivedView.gameObject;
                controller.Dispose();
                Object.Destroy(viewGameObject);
            }

            _controllers.Clear();
        }
    }
}