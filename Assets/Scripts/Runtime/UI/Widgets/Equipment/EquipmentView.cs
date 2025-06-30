using System.Linq;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.UI.Core.Components;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Equipment.Item;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Equipment
{
    public sealed class EquipmentView : BaseView<EquipmentViewModel>
    {
        [SerializeField] private DynamicCollectionView<EquipmentItemSlotView> _slotsView;
        [SerializeField] private ViewFactory<EquipmentItemView> _itemsFactory;

        protected override void Initialize(EquipmentViewModel viewModel)
        {
            viewModel.ActiveSlotsSizeProperty.Subscribe(OnActiveSlotsSizeChanged).AddTo(CompositeDisposable);
            viewModel.ItemsProperty.ObserveAddWithInitial().Subscribe(OnItemAdded).AddTo(CompositeDisposable);
            viewModel.ItemsProperty.ObserveRemove().Subscribe(OnItemRemoved).AddTo(CompositeDisposable);
        }

        private void OnItemAdded(CollectionAddEvent<EquipmentItemViewModel> addEvent)
        {
            var index = addEvent.Index;
            var viewModel = addEvent.Value;

            if (_slotsView.Count() <= index)
            {
                return;
            }

            var slotView = _slotsView.ElementAt(index);
            var itemView = _itemsFactory.CreateView(slotView.RectTransform);
            itemView.Initialize(viewModel);
            slotView.SetItem(itemView);
        }

        private void OnItemRemoved(CollectionRemoveEvent<EquipmentItemViewModel> removeEvent)
        {
            var removalIndex = removeEvent.Index;

            if (_slotsView.Count() <= removalIndex)
            {
                return;
            }

            var slotViewToRemove = _slotsView.ElementAt(removalIndex);
            var itemViewToRemove = slotViewToRemove.ItemView;
            slotViewToRemove.Clear();
            _itemsFactory.DestroyView(itemViewToRemove);

            for (var i = removalIndex; i < _slotsView.Count(); i++)
            {
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

        private void OnActiveSlotsSizeChanged(float size)
        {
            var roundedSize = Mathf.RoundToInt(size);

            var existingItemViewModels = _slotsView
                .Where(slot => !slot.IsEmpty)
                .Select(slot => slot.ItemView.ViewModel)
                .ToList();

            _slotsView.Clear();

            for (var i = 0; i < roundedSize; i++)
            {
                _slotsView.Add();
            }

            for (var i = 0; i < existingItemViewModels.Count; i++)
            {
                var viewModel = existingItemViewModels[i];
                OnItemAdded(new CollectionAddEvent<EquipmentItemViewModel>(i, viewModel));
            }
        }
    }
}