using System.Linq;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.UI.Core.Components;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Item;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory
{
    public sealed class InventoryView : BaseView<InventoryViewModel>
    {
        [SerializeField] private ViewFactory<InventoryItemView> _itemViewFactory;
        [SerializeField] private DynamicCollectionView<ItemSlotView> _slotsView;

        protected override void Initialize(InventoryViewModel viewModel)
        {
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
            var removalIndex = removeEvent.Index;

            if (_slotsView.Count() <= removalIndex)
            {
                return;
            }

            var slotViewToRemove = _slotsView.ElementAt(removalIndex);
            var itemViewToRemove = slotViewToRemove.ItemView;
            slotViewToRemove.Clear();
            _itemViewFactory.DestroyView(itemViewToRemove);

            for (var i = 0; i < _slotsView.Count(); i++)
            {
                if (i <= removalIndex)
                {
                    continue;
                }

                var slotView = _slotsView.ElementAt(i);
                if (slotView.IsEmpty)
                {
                    continue;
                }

                var itemView = slotView.ItemView;
                var prevSlotView = _slotsView.ElementAt(i - 1);
                prevSlotView.SetItem(itemView);
                slotView.Clear();
            }
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