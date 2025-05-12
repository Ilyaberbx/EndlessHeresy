using System.Threading.Tasks;
using EndlessHeresy.UI.Core;

namespace EndlessHeresy.UI.Services.Modals
{
    public interface IModalsService
    {
        Task<TController> ShowAsync<TController, TModel>(TModel model)
            where TController : BaseController<TModel>, new()
            where TModel : IModel;

        void HideAll();
    }
} 