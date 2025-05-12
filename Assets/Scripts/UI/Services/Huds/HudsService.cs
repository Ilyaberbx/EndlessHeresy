using System.Threading.Tasks;
using EndlessHeresy.Global.Services.AssetsManagement;
using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.Core;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.UI.Services.Huds
{
    public sealed class HudsService : IHudsService
    {
        private const string ControllerPostfix = "Controller";
        private const string ViewPathFormat = "Huds/{0}";

        private readonly UIControllerManager _manager;

        public HudsService(Transform root, IAssetsService assetsService, IObjectResolver resolver)
        {
            _manager = new UIControllerManager(root, assetsService, resolver);
        }

        public Task<TController> ShowAsync<TController, TModel>(TModel model, ShowType showType)
            where TController : BaseController<TModel>, new()
            where TModel : IModel
        {
            var controllerName = typeof(TController).Name;
            var viewKey = controllerName.Replace(ControllerPostfix, string.Empty);
            var address = string.Format(ViewPathFormat, viewKey);
            bool additive = showType == ShowType.Additive;
            return _manager.ShowAsync<TController, TModel>(model, address, additive);
        }

        public void HideAll()
        {
            _manager.HideAll();
        }
    }
}