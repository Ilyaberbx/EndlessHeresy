using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Attributes.Item;
using UniRx;
using Unity.VisualScripting;
using Attribute = EndlessHeresy.Runtime.Attributes.Attribute;

namespace EndlessHeresy.Runtime.UI.Widgets.Attributes
{
    public sealed class AttributesViewModel : BaseViewModel<AttributesModel>
    {
        private readonly IViewModelFactory _factory;
        private readonly Dictionary<Attribute, AttributeItemViewModel> _map;
        public IReactiveCollection<AttributeItemViewModel> ItemsProperty { get; }

        public AttributesViewModel(IViewModelFactory factory)
        {
            _factory = factory;
            _map = new Dictionary<Attribute, AttributeItemViewModel>();
            ItemsProperty = new ReactiveCollection<AttributeItemViewModel>();
        }

        protected override void Initialize(AttributesModel model)
        {
            Model.Attributes.ObserveAdd().Subscribe(OnAttributeAdded).AddTo(CompositeDisposable);
            Model.Attributes.ObserveRemove().Subscribe(OnAttributeRemoved).AddTo(CompositeDisposable);

            ItemsProperty.AddRange(Model
                .Attributes
                .Select(CreateItemViewModel));
        }

        private AttributeItemViewModel CreateItemViewModel(Attribute attribute)
        {
            var model = new AttributeItemModel(attribute);
            return _factory.Create<AttributeItemViewModel, AttributeItemModel>(model);
        }

        private void OnAttributeAdded(CollectionAddEvent<Attribute> addEvent)
        {
            var attribute = addEvent.Value;
            var viewModel = CreateItemViewModel(attribute);
            _map.Add(attribute, viewModel);
            ItemsProperty.Add(viewModel);
        }

        private void OnAttributeRemoved(CollectionRemoveEvent<Attribute> removeEvent)
        {
            var statusEffect = removeEvent.Value;

            if (_map.Remove(statusEffect, out var viewModel))
            {
                ItemsProperty.Remove(viewModel);
            }
        }
    }
}