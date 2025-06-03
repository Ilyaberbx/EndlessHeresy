using System.Linq;
using EndlessHeresy.Runtime.UI.Core.Components;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Item;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Item.Info;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory
{
    public sealed class InventoryView : BaseView<InventoryViewModel>
    {
        [SerializeField] private CollectionView<InventoryItemView> _itemsView;
        [SerializeField] private InventoryItemInfoView _itemInfoView;

        protected override void Initialize(InventoryViewModel viewModel)
        {
            _itemInfoView.Initialize(viewModel.InfoViewModel);

            viewModel.ItemsProperty.ObserveAdd().Subscribe(OnItemAdded).AddTo(CompositeDisposable);
            viewModel.ItemsProperty.ObserveRemove().Subscribe(OnItemRemoved).AddTo(CompositeDisposable);
        }

        private void OnItemAdded(CollectionAddEvent<InventoryItemViewModel> addEvent)
        {
            _itemsView.Add().Initialize(addEvent.Value);
        }

        private void OnItemRemoved(CollectionRemoveEvent<InventoryItemViewModel> removeEvent)
        {
            var viewToRemove = _itemsView.FirstOrDefault(view => view.ViewModel == removeEvent.Value);
            _itemsView.Remove(viewToRemove);
        }
    }
}