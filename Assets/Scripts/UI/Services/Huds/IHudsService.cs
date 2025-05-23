using System.Threading.Tasks;
using EndlessHeresy.UI.Core;
using EndlessHeresy.UI.Core.MVVM;

namespace EndlessHeresy.UI.Services.Huds
{
    public interface IHudsService
    {
        Task<TViewModel> ShowAsync<TViewModel, TModel>(TModel model, ShowType showType)
            where TViewModel : BaseViewModel<TModel>, new()
            where TModel : IModel;

        void HideAll();
    }
}