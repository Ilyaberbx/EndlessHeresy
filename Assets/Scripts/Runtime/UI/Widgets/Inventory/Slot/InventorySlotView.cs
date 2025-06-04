using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Item;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory.Slot
{
    public sealed class InventorySlotView : BaseView
    {
        public InventoryItemView ItemView { get; private set; }
        public bool IsEmpty => ItemView == null;

        public void SetItem(InventoryItemView itemView)
        {
            ItemView = itemView;
            ItemView.RectTransform.localPosition = Vector3.zero;
        }

        public void Clear()
        {
            ItemView = null;
        }
    }
}