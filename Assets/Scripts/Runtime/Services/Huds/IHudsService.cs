using System.Threading.Tasks;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.Services.Huds
{
    public interface IHudsService
    {
        Task<TViewModel> ShowAsync<TViewModel, TModel>(TModel model, ShowType showType)
            where TViewModel : BaseViewModel<TModel>, new()
            where TModel : IModel;

        void HideAll();
    }
}