using System.Threading.Tasks;
using EndlessHeresy.Runtime.Services.AssetsManagement;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Services.Huds
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
            where TController : BaseViewModel<TModel>, new()
            where TModel : IModel
        {
            var controllerName = typeof(TController).Name;
            var viewKey = controllerName.Replace(ControllerPostfix, string.Empty);
            var address = string.Format(ViewPathFormat, viewKey);
            return _manager.ShowAsync<TController, TModel>(model, address, showType);
        }

        public void HideAll()
        {
            _manager.HideAll();
        }
    }
}