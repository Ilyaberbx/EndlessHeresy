using System.Threading.Tasks;
using EndlessHeresy.Runtime.UI.Core;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Services.Huds
{
    public interface IHudsService
    {
        Task<TViewModel> ShowAsync<TViewModel, TModel>(TModel model, ShowType showType)
            where TViewModel : BaseViewModel<TModel>
            where TModel : IModel;

        void UpdateFactory(IViewModelFactory factory);
        void HideAll();
    }
}