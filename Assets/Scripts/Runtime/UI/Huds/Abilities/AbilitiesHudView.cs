using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.UI.Core.Components;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.Abilities.Item;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Huds.Abilities
{
    public sealed class AbilitiesHudView : BaseView<AbilitiesHudViewModel>
    {
        [SerializeField] private StaticCollectionView<AbilityItemView> _itemsView;

        protected override void Initialize(AbilitiesHudViewModel viewModel)
        {
            ViewModel.AbilitiesProperty
                .ObserveAddWithInitial()
                .Subscribe(OnAbilityItemAdded)
                .AddTo(CompositeDisposable);
        }

        private void OnAbilityItemAdded(CollectionAddEvent<AbilityItemViewModel> addEvent)
        {
            var viewModel = addEvent.Value;
            var index = addEvent.Index;

            if (!_itemsView.TryGet(index, out var view))
            {
                return;
            }

            view.Initialize(viewModel);
        }
    }
}