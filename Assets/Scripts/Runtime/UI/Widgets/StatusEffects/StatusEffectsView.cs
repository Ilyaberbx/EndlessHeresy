using System.Linq;
using EndlessHeresy.Runtime.UI.Core.Components;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.StatusEffects.Item;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.StatusEffects
{
    public sealed class StatusEffectsView : BaseView<StatusEffectsViewModel>
    {
        [SerializeField] private CollectionView<StatusEffectItemView> _itemsView;

        protected override void Initialize(StatusEffectsViewModel viewModel)
        {
            viewModel.ItemsProperty.ObserveAdd().Subscribe(OnItemAdded).AddTo(CompositeDisposable);
            viewModel.ItemsProperty.ObserveRemove().Subscribe(OnItemRemoved).AddTo(CompositeDisposable);
        }

        private void OnItemAdded(CollectionAddEvent<StatusEffectItemViewModel> addEvent)
        {
            _itemsView.Add().Initialize(addEvent.Value);
        }

        private void OnItemRemoved(CollectionRemoveEvent<StatusEffectItemViewModel> removeEvent)
        {
            var viewToRemove = _itemsView.FirstOrDefault(temp => temp.ViewModel == removeEvent.Value);
            _itemsView.Remove(viewToRemove);
        }
    }
}