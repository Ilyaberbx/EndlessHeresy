using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.StatusEffects;

namespace EndlessHeresy.Runtime.UI.Huds.StatusEffects
{
    public sealed class StatusEffectsHudViewModel : BaseViewModel<StatusEffectsHudModel>
    {
        private readonly IViewModelFactory _factory;
        public StatusEffectsViewModel ItemsViewModel { get; private set; }

        public StatusEffectsHudViewModel(IViewModelFactory factory)
        {
            _factory = factory;
        }

        protected override void Initialize(StatusEffectsHudModel model)
        {
            ItemsViewModel = _factory.Create<StatusEffectsViewModel,
                StatusEffectsModel>(model.StatusEffectsModel);
        }

        protected override void OnDispose()
        {
            ItemsViewModel.Dispose();
        }
    }
}