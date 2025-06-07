using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Inventory.Items.Implementations;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Equipment.Item;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Widgets.Equipment
{
    public sealed class EquipmentViewModel : BaseViewModel<EquipmentModel>
    {
        public event Action<ItemRoot> OnSelected;
        private readonly Dictionary<ItemRoot, EquipmentItemViewModel> _itemViewModels;
        private readonly Dictionary<ItemRoot, CompositeDisposable> _itemSubscriptions;
        private readonly ReactiveCollection<EquipmentItemViewModel> _itemsProperty;
        private readonly IViewModelFactory _factory;

        public IReactiveCollection<EquipmentItemViewModel> ItemsProperty => _itemsProperty;
        public IReadOnlyReactiveProperty<float> ActiveSlotsSizeProperty { get; private set; }

        public EquipmentViewModel(IViewModelFactory factory)
        {
            _factory = factory;
            _itemViewModels = new Dictionary<ItemRoot, EquipmentItemViewModel>();
            _itemSubscriptions = new Dictionary<ItemRoot, CompositeDisposable>();
            _itemsProperty = new ReactiveCollection<EquipmentItemViewModel>();
        }

        protected override void Initialize(EquipmentModel model)
        {
            SubscribeToItemCollection(model);
            UpdateActiveSlotsSizeProperty(model);
        }

        private void UpdateActiveSlotsSizeProperty(EquipmentModel model)
        {
            var stats = model.Owner.GetComponent<StatsComponent>();
            var activeEquipmentSlotsStat = stats.GetStat(StatType.ActiveEquipmentSlots);
            ActiveSlotsSizeProperty = activeEquipmentSlotsStat.ProcessedValueProperty;
        }

        private void SubscribeToItemCollection(EquipmentModel model)
        {
            model.Items.ObserveAddWithInitial().Subscribe(OnItemAdded).AddTo(CompositeDisposable);
            model.Items.ObserveRemove().Subscribe(OnItemRemoved).AddTo(CompositeDisposable);
            model.Items.ObserveReset().Subscribe(OnItemsReset).AddTo(CompositeDisposable);
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
                return;
            }

            SubscribeToEquipableState(item, equipableComponent);
        }

        private void OnItemRemoved(CollectionRemoveEvent<ItemRoot> removeEvent)
        {
            var item = removeEvent.Value;
            DisposeItemSubscription(item);
            RemoveItemViewModel(item);
        }

        private void OnItemsReset(Unit unit)
        {
            foreach (var subscription in _itemSubscriptions.Values)
            {
                subscription.Dispose();
            }

            _itemSubscriptions.Clear();
            _itemViewModels.Clear();
            _itemsProperty.Clear();
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
                AddItemViewModel(item);
                return;
            }

            RemoveItemViewModel(item);
        }

        private void AddItemViewModel(ItemRoot item)
        {
            if (_itemViewModels.ContainsKey(item))
            {
                return;
            }

            var model = new EquipmentItemModel(item, Model.Owner);
            var viewModel = _factory.Create<EquipmentItemViewModel, EquipmentItemModel>(model);
            viewModel.OnSelected += OnItemSelected;
            _itemViewModels.Add(item, viewModel);
            _itemsProperty.Add(viewModel);
        }

        private void RemoveItemViewModel(ItemRoot item)
        {
            if (_itemViewModels.Remove(item, out var viewModel))
            {
                _itemsProperty.Remove(viewModel);
                viewModel.OnSelected -= OnItemSelected;
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
            OnSelected?.Invoke(item);
        }

        public void DeselectAll()
        {
            foreach (var viewModel in _itemViewModels.Values)
            {
                viewModel.Deselect();
            }
        }
    }
}