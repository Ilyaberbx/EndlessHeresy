using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.UI.Core.Factory
{
    public interface IViewModelFactory
    {
        TViewModel Create<TViewModel, TModel>(TModel model)
            where TViewModel : BaseViewModel<TModel> where TModel : IModel;
    }
}