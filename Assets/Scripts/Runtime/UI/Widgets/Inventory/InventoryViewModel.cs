using System.Collections.Generic;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Item;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Item.Info;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory
{
    public sealed class InventoryViewModel : BaseViewModel<InventoryModel>
    {
        private readonly IViewModelFactory _factory;
        private readonly Dictionary<ItemRoot, InventoryItemViewModel> _itemViewModels;
        public IReactiveCollection<InventoryItemViewModel> ItemsProperty { get; }
        public IReactiveProperty<int> InventorySizeProperty { get; }
        public InventoryItemInfoViewModel InfoViewModel { get; private set; }

        public InventoryViewModel(IViewModelFactory factory)
        {
            _factory = factory;
            _itemViewModels = new Dictionary<ItemRoot, InventoryItemViewModel>();
            ItemsProperty = new ReactiveCollection<InventoryItemViewModel>();
            InventorySizeProperty = new ReactiveProperty<int>();
        }

        protected override void Initialize(InventoryModel model)
        {
            Model.Items.ObserveAdd().Subscribe(OnItemAdded).AddTo(CompositeDisposable);
            Model.Items.ObserveRemove().Subscribe(OnItemRemoved).AddTo(CompositeDisposable);
            InfoViewModel = _factory.Create<InventoryItemInfoViewModel>();
            InventorySizeProperty.Value = Model.MaxSize;
        }

        private void OnItemAdded(CollectionAddEvent<ItemRoot> addEvent)
        {
            var item = addEvent.Value;
            var model = new InventoryItemModel(item, Model.Owner);
            var viewModel = _factory.Create<InventoryItemViewModel, InventoryItemModel>(model);
            viewModel.OnSelected += OnItemSelected;

            _itemViewModels.Add(item, viewModel);
            ItemsProperty.Add(viewModel);
        }

        private void OnItemRemoved(CollectionRemoveEvent<ItemRoot> removeEvent)
        {
            var item = removeEvent.Value;

            if (_itemViewModels.Remove(item, out var viewModel))
            {
                viewModel.OnSelected -= OnItemSelected;
                ItemsProperty.Remove(viewModel);
            }
        }

        private void OnItemSelected(ItemRoot item)
        {
            InfoViewModel.Deselect();
            InfoViewModel.Select(item);
        }
    }
}