using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Inventory.Item;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Widgets.Inventory
{
    public sealed class InventoryViewModel : BaseViewModel<InventoryModel>
    {
        public event Action<ItemRoot> OnSelected;

        private readonly IViewModelFactory _factory;
        private readonly Dictionary<ItemRoot, InventoryItemViewModel> _itemViewModels;
        private readonly Dictionary<ItemRoot, CompositeDisposable> _itemSubscriptions;
        public IReactiveCollection<InventoryItemViewModel> ItemsProperty { get; }
        public IReactiveProperty<int> InventorySizeProperty { get; }

        public InventoryViewModel(IViewModelFactory factory)
        {
            _factory = factory;
            _itemViewModels = new Dictionary<ItemRoot, InventoryItemViewModel>();
            _itemSubscriptions = new Dictionary<ItemRoot, CompositeDisposable>();
            ItemsProperty = new ReactiveCollection<InventoryItemViewModel>();
            InventorySizeProperty = new ReactiveProperty<int>();
        }

        protected override void Initialize(InventoryModel model)
        {
            var items = Model.Items;

            items.ObserveAddWithInitial().Subscribe(OnItemAdded).AddTo(CompositeDisposable);
            items.ObserveRemove().Subscribe(OnItemRemoved).AddTo(CompositeDisposable);

            InventorySizeProperty.Value = Model.MaxSize;
        }

        protected override void OnDispose()
        {
            foreach (var subscription in _itemSubscriptions.Values)
            {
                subscription.Dispose();
            }

            _itemSubscriptions.Clear();

            foreach (var viewModel in _itemViewModels.Values)
            {
                viewModel.OnSelected -= OnItemSelected;
            }
        }

        public void DeselectAll()
        {
            foreach (var viewModel in _itemViewModels.Values)
            {
                viewModel.Deselect();
            }
        }

        private void OnItemAdded(CollectionAddEvent<ItemRoot> addEvent)
        {
            var item = addEvent.Value;

            if (_itemSubscriptions.ContainsKey(item))
            {
                return;
            }

            var equipableComponent = item.Components.OfType<EquipableItemComponent>().FirstOrDefault();


            if (equipableComponent == null)
            {
                AddItemViewModel(item);
                return;
            }

            SubscribeToEquipableState(item, equipableComponent);
        }

        private void SubscribeToEquipableState(ItemRoot item, EquipableItemComponent equipableComponent)
        {
            var subscription = new CompositeDisposable();
            _itemSubscriptions[item] = subscription;

            equipableComponent.IsEquipped
                .Subscribe(isEquipped => OnEquipableStateChanged(item, isEquipped))
                .AddTo(subscription);
        }

        private void OnEquipableStateChanged(ItemRoot item, bool isEquipped)
        {
            if (isEquipped)
            {
                RemoveItemViewModel(item);
                return;
            }

            AddItemViewModel(item);
        }

        private void OnItemRemoved(CollectionRemoveEvent<ItemRoot> removeEvent)
        {
            var item = removeEvent.Value;
            DisposeItemSubscription(item);
            RemoveItemViewModel(item);
        }

        private void AddItemViewModel(ItemRoot item)
        {
            if (_itemViewModels.ContainsKey(item))
            {
                return;
            }

            var model = new InventoryItemModel(item, Model.Owner);
            var viewModel = _factory.Create<InventoryItemViewModel, InventoryItemModel>(model);
            viewModel.OnSelected += OnItemSelected;

            _itemViewModels.Add(item, viewModel);
            ItemsProperty.Add(viewModel);
        }

        private void RemoveItemViewModel(ItemRoot item)
        {
            if (_itemViewModels.Remove(item, out var viewModel))
            {
                viewModel.OnSelected -= OnItemSelected;
                ItemsProperty.Remove(viewModel);
            }
        }

        private void DisposeItemSubscription(ItemRoot item)
        {
            if (_itemSubscriptions.Remove(item, out var subscription))
            {
                subscription.Dispose();
            }
        }

        private void OnItemSelected(ItemRoot item)
        {
            foreach (var viewModel in _itemViewModels.Values.Where(temp => temp.Model.Item != item))
            {
                viewModel.Deselect();
            }

            OnSelected?.Invoke(item);
        }
    }
}