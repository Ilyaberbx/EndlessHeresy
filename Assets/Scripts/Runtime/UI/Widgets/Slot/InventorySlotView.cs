using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Slot
{
    public abstract class InventorySlotView<TItemView, TViewModel> : BaseView<TViewModel>
        where TViewModel : BaseViewModel
        where TItemView : ItemCoreView<TViewModel>
    {
        public TItemView ItemView { get; private set; }
        public bool IsEmpty => ItemView == null;

        public void SetItem(TItemView itemView)
        {
            ItemView = itemView;
            ItemView.RectTransform.SetParent(transform);
            ItemView.RectTransform.localPosition = Vector3.zero;
        }

        public void Clear()
        {
            ItemView = null;
        }
    }
}