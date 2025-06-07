using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Item;
using EndlessHeresy.Runtime.UI.Widgets.ItemSelection;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory.Slot
{
    public sealed class InventorySlotView<TItemView, TViewModel> : BaseView<TViewModel>
        where TViewModel : BaseViewModel
        where TItemView : ItemCoreView<TViewModel>
    {
        public TItemView ItemView { get; private set; }
        public bool IsEmpty => ItemView == null;

        public void SetItem(TItemView itemView)
        {
            ItemView = itemView;
            ItemView.Root.SetParent(transform);
            ItemView.Root.localPosition = Vector3.zero;
        }

        public void Clear()
        {
            ItemView = null;
        }
    }
}