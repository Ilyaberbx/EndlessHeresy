using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using TMPro;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Huds.Cheats
{
    public sealed class CheatsHudView : BaseView<CheatsHudViewModel>
    {
        [SerializeField] private TMP_Dropdown _dropdown;

        protected override void Initialize(CheatsHudViewModel viewModel)
        {
            viewModel.AvailableItemsProperty
                .ObserveAddWithInitial()
                .Subscribe(OnAvailableItemAdded)
                .AddTo(CompositeDisposable);
        }

        private void OnAvailableItemAdded(CollectionAddEvent<ItemType> addEvent)
        {
            _dropdown.options.Add(new TMP_Dropdown.OptionData()
            {
                text = addEvent.Value.ToString()
            });
        }
    }
}