using System.Threading.Tasks;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Services.Modals
{
    public interface IModalsService
    {
        Task<TViewModel> ShowAsync<TViewModel, TModel>(TModel model)
            where TViewModel : BaseViewModel<TModel>
            where TModel : IModel;

        void UpdateFactory(IViewModelFactory factory);
        void HideAll();
        public void HideAllOfType<TViewModel>() where TViewModel : BaseViewModel;
    }
}