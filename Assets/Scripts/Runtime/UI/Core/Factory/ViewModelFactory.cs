using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using VContainer;

namespace EndlessHeresy.Runtime.UI.Core.Factory
{
    public sealed class ViewModelFactory : IViewModelFactory
    {
        private readonly IObjectResolver _resolver;

        public ViewModelFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public TViewModel Create<TViewModel, TModel>(TModel model)
            where TViewModel : BaseViewModel<TModel>
            where TModel : IModel
        {
            var viewModel = _resolver.Instantiate<TViewModel>(Lifetime.Scoped);
            viewModel.Initialize(model);
            return viewModel;
        }
    }
}