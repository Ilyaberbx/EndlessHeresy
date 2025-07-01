using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Common;
using EndlessHeresy.Runtime.UI.Widgets.Equipment;
using EndlessHeresy.Runtime.UI.Widgets.Inventory;
using EndlessHeresy.Runtime.UI.Widgets.ItemInfo;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Modals.Inventory
{
    public sealed class InventoryModalView : BaseView<InventoryModalViewModel>
    {
        [SerializeField] private CloseModalWindowView _closeWindowView;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private InventoryItemInfoView _itemInfoView;
        [SerializeField] private EquipmentView _equipmentView;

        protected override void Initialize(InventoryModalViewModel viewModel)
        {
            _closeWindowView.Initialize(viewModel.CloseWindowViewModel);
            _inventoryView.Initialize(viewModel.InventoryViewModel);
            _itemInfoView.Initialize(viewModel.ItemInfoViewModel);
            _equipmentView.Initialize(viewModel.EquipmentViewModel);
        }
    }
}