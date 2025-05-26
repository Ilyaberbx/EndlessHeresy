using System.Threading.Tasks;
using EndlessHeresy.Meta.UI.Core;
using EndlessHeresy.Meta.UI.Core.MVVM;

namespace EndlessHeresy.Meta.UI.Services.Huds
{
    public interface IHudsService
    {
        Task<TViewModel> ShowAsync<TViewModel, TModel>(TModel model, ShowType showType)
            where TViewModel : BaseViewModel<TModel>, new()
            where TModel : IModel;

        void HideAll();
    }
}