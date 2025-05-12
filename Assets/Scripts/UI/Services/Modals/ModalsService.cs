using System.Threading.Tasks;
using EndlessHeresy.Global.Services.AssetsManagement;
using EndlessHeresy.UI.Core;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.UI.Services.Modals
{
    public sealed class ModalsService : IModalsService
    {
        private const string ControllerPostfix = "Controller";
        private const string ViewPathFormat = "Modals/{0}";

        private readonly UIControllerManager _manager;

        public ModalsService(Transform root, IAssetsService assetsService, IObjectResolver resolver)
        {
            _manager = new UIControllerManager(root, assetsService, resolver);
        }

        public Task<TController> ShowAsync<TController, TModel>(TModel model)
            where TController : BaseController<TModel>, new()
            where TModel : IModel
        {
            var controllerName = typeof(TController).Name;
            var viewKey = controllerName.Replace(ControllerPostfix, string.Empty);
            var address = string.Format(ViewPathFormat, viewKey);
            return _manager.ShowAsync<TController, TModel>(model, address, ShowType.Single);
        }

        public void HideAll()
        {
            _manager.HideAll();
        }
    }
} 