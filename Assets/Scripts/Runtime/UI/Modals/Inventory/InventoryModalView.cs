using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Attributes;
using EndlessHeresy.Runtime.UI.Widgets.Common;
using EndlessHeresy.Runtime.UI.Widgets.Inventory;
using EndlessHeresy.Runtime.UI.Widgets.ItemInfo;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Modals.Inventory
{
    public sealed class InventoryModalView : BaseView<InventoryModalViewModel>
    {
        [SerializeField] private CloseModalWindowView _closeWindowView;
        [SerializeField] private AttributesView _attributesView;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private InventoryItemInfoView _itemInfoView;

        protected override void Initialize(InventoryModalViewModel viewModel)
        {
            _attributesView.Initialize(viewModel.AttributesViewModel);
            _closeWindowView.Initialize(viewModel.CloseWindowViewModel);
            _inventoryView.Initialize(viewModel.InventoryViewModel);
            _itemInfoView.Initialize(viewModel.ItemInfoViewModel);
        }
    }
}