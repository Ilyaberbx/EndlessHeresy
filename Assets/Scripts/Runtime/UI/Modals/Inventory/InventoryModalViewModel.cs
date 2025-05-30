using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Attributes;
using EndlessHeresy.Runtime.UI.Widgets.Common;

namespace EndlessHeresy.Runtime.UI.Modals.Inventory
{
    public sealed class InventoryModalViewModel : BaseViewModel<InventoryModalModel>
    {
        private readonly IViewModelFactory _factory;
        public AttributesViewModel AttributesViewModel { get; private set; }
        public CloseModalWindowViewModel CloseWindowViewModel { get; private set; }

        public InventoryModalViewModel(IViewModelFactory factory)
        {
            _factory = factory;
        }

        protected override void Initialize(InventoryModalModel model)
        {
            base.Initialize(model);

            AttributesViewModel = _factory.Create<AttributesViewModel, AttributesModel>(Model.AttributesModel);
            CloseWindowViewModel = _factory.Create<CloseModalWindowViewModel>();
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            AttributesViewModel.Dispose();
        }
    }
}