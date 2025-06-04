using System.Linq;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.UI.Core.Components;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Item;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Item.Info;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Slot;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory
{
    public sealed class InventoryView : BaseView<InventoryViewModel>
    {
        [SerializeField] private ViewFactory<InventoryItemView> _itemViewFactory;
        [SerializeField] private CollectionView<InventorySlotView> _slotsView;
        [SerializeField] private InventoryItemInfoView _itemInfoView;

        protected override void Initialize(InventoryViewModel viewModel)
        {
            _itemInfoView.Initialize(viewModel.InfoViewModel);

            viewModel.InventorySizeProperty.Subscribe(OnInventorySizeChanged).AddTo(CompositeDisposable);
            viewModel.ItemsProperty.ObserveAddWithInitial().Subscribe(OnItemAdded).AddTo(CompositeDisposable);
            viewModel.ItemsProperty.ObserveRemove().Subscribe(OnItemRemoved).AddTo(CompositeDisposable);
        }

        private void OnItemAdded(CollectionAddEvent<InventoryItemViewModel> addEvent)
        {
            var index = addEvent.Index;
            var viewModel = addEvent.Value;

            if (_slotsView.Count() <= index)
            {
                return;
            }

            var slotView = _slotsView.ElementAt(index);
            var itemView = _itemViewFactory.CreateView(slotView.RectTransform);
            itemView.Initialize(viewModel);
            slotView.SetItem(itemView);
        }

        private void OnItemRemoved(CollectionRemoveEvent<InventoryItemViewModel> removeEvent)
        {
            var index = removeEvent.Index;

            if (_slotsView.Count() <= index)
            {
                return;
            }

            var slotView = _slotsView.ElementAt(index);
            if (slotView.IsEmpty)
            {
                return;
            }

            _itemViewFactory.DestroyView(slotView.ItemView);
            slotView.Clear();
        }

        private void OnInventorySizeChanged(int size)
        {
            for (var i = 0; i < size; i++)
            {
                _slotsView.Add();
            }
        }
    }
}