using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Attributes;
using EndlessHeresy.Runtime.UI.Widgets.Common;
using EndlessHeresy.Runtime.UI.Widgets.Equipment;
using EndlessHeresy.Runtime.UI.Widgets.Inventory;
using EndlessHeresy.Runtime.UI.Widgets.ItemInfo;

namespace EndlessHeresy.Runtime.UI.Modals.Inventory
{
    public sealed class InventoryModalViewModel : BaseViewModel<InventoryModalModel>
    {
        private readonly IViewModelFactory _factory;
        public AttributesViewModel AttributesViewModel { get; private set; }
        public CloseModalWindowViewModel CloseWindowViewModel { get; private set; }
        public InventoryViewModel InventoryViewModel { get; private set; }
        public InventoryItemInfoViewModel ItemInfoViewModel { get; private set; }
        public EquipmentViewModel EquipmentViewModel { get; private set; }

        public InventoryModalViewModel(IViewModelFactory factory)
        {
            _factory = factory;
        }

        protected override void Initialize(InventoryModalModel model)
        {
            base.Initialize(model);

            AttributesViewModel = _factory.Create<AttributesViewModel, AttributesModel>(Model.AttributesModel);
            CloseWindowViewModel = _factory.Create<CloseModalWindowViewModel>();
            InventoryViewModel = _factory.Create<InventoryViewModel, InventoryModel>(Model.InventoryModel);
            ItemInfoViewModel = _factory.Create<InventoryItemInfoViewModel>();
            EquipmentViewModel = _factory.Create<EquipmentViewModel, EquipmentModel>(Model.EquipmentModel);
            InventoryViewModel.OnSelected += OnInventoryItemSelected;
            EquipmentViewModel.OnSelected += OnEquipmentItemSelected;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            InventoryViewModel.OnSelected -= OnInventoryItemSelected;
            EquipmentViewModel.OnSelected -= OnEquipmentItemSelected;
            AttributesViewModel.Dispose();
            CloseWindowViewModel.Dispose();
            InventoryViewModel.Dispose();
            ItemInfoViewModel.Dispose();
        }

        private void OnEquipmentItemSelected(ItemRoot item)
        {
            InventoryViewModel.DeselectAll();
            ItemInfoViewModel.Select(item);
        }

        private void OnInventoryItemSelected(ItemRoot item)
        {
            EquipmentViewModel.DeselectAll();
            ItemInfoViewModel.Select(item);
        }
    }
}