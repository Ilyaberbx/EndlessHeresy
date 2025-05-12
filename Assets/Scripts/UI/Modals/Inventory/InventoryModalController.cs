using EndlessHeresy.UI.Core;
using EndlessHeresy.UI.Widgets.Attributes;
using VContainer;

namespace EndlessHeresy.UI.Modals.Inventory
{
    public sealed class InventoryModalController : BaseController<InventoryModalModel, InventoryModalView>
    {
        private AttributesWidgetController _attributesWidgetController;
        private IObjectResolver _resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) => _resolver = resolver;

        protected override void Show(InventoryModalModel model, InventoryModalView view)
        {
            base.Show(model, view);

            _attributesWidgetController = new AttributesWidgetController();
            _resolver.Inject(_attributesWidgetController);
            _attributesWidgetController.Initialize(View.AttributesWidgetView, Model.GetAttributesWidgetModel());
        }

        protected override void Hide()
        {
            base.Hide();

            _attributesWidgetController.Dispose();
        }
    }
}