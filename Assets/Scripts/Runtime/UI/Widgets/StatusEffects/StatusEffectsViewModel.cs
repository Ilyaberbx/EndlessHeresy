using System.Collections.Generic;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.UI.Core.Factory;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.StatusEffects.Item;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Widgets.StatusEffects
{
    public sealed class StatusEffectsViewModel : BaseViewModel<StatusEffectsModel>
    {
        private readonly IViewModelFactory _factory;
        private readonly Dictionary<IStatusEffectRoot, StatusEffectItemViewModel> _map;
        public IReactiveCollection<StatusEffectItemViewModel> ItemsProperty { get; }

        public StatusEffectsViewModel(IViewModelFactory factory)
        {
            _factory = factory;
            _map = new Dictionary<IStatusEffectRoot, StatusEffectItemViewModel>();
            ItemsProperty = new ReactiveCollection<StatusEffectItemViewModel>();
        }

        protected override void Initialize(StatusEffectsModel model)
        {
            Model.StatusEffects.ObserveAdd().Subscribe(OnStatusEffectAdded).AddTo(CompositeDisposable);
            Model.StatusEffects.ObserveRemove().Subscribe(OnStatusEffectRemoved).AddTo(CompositeDisposable);
        }

        private void OnStatusEffectAdded(CollectionAddEvent<IStatusEffectRoot> addEvent)
        {
            var statusEffect = addEvent.Value;
            var model = new StatusEffectItemModel(statusEffect);
            var viewModel = _factory.Create<StatusEffectItemViewModel, StatusEffectItemModel>(model);
            _map.Add(statusEffect, viewModel);
            ItemsProperty.Add(viewModel);
        }

        private void OnStatusEffectRemoved(CollectionRemoveEvent<IStatusEffectRoot> removeEvent)
        {
            var statusEffect = removeEvent.Value;

            if (_map.Remove(statusEffect, out var viewModel))
            {
                ItemsProperty.Remove(viewModel);
            }
        }
    }
}