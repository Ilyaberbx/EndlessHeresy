using System.Linq;
using EndlessHeresy.Runtime.UI.Core.Components;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Attributes.Item;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Attributes
{
    public sealed class AttributesView : BaseView<AttributesViewModel>
    {
        [SerializeField] private CollectionView<AttributeItemView> _itemsView;

        protected override void Initialize(AttributesViewModel viewModel)
        {
            viewModel.ItemsProperty.ObserveAdd().Subscribe(OnItemAdded).AddTo(CompositeDisposable);
            viewModel.ItemsProperty.ObserveRemove().Subscribe(OnItemRemoved).AddTo(CompositeDisposable);
        }

        private void OnItemAdded(CollectionAddEvent<AttributeItemViewModel> addEvent)
        {
            _itemsView.Add().Initialize(addEvent.Value);
        }

        private void OnItemRemoved(CollectionRemoveEvent<AttributeItemViewModel> removeEvent)
        {
            var viewToRemove = _itemsView.FirstOrDefault(temp => temp.ViewModel == removeEvent.Value);
            _itemsView.Remove(viewToRemove);
        }
    }
}