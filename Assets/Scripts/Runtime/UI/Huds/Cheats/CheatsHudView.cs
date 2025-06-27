using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Huds.Cheats
{
    public sealed class CheatsHudView : BaseView<CheatsHudViewModel>
    {
        [SerializeField] private TMP_Dropdown _itemsDropdown;
        [SerializeField] private Button _addItemButton;
        [SerializeField] private TMP_Dropdown _statusEffectsDropdown;
        [SerializeField] private Button _addStatusEffectButton;
        private int _selectedItemOptionIndex;
        private int _selectedStatusEffectIndex;

        protected override void Initialize(CheatsHudViewModel viewModel)
        {
            viewModel.AvailableItemsProperty
                .ObserveAddWithInitial()
                .Subscribe(OnAvailableItemAdded)
                .AddTo(CompositeDisposable);

            viewModel.AvailableStatusEffectsProperty
                .ObserveAddWithInitial()
                .Subscribe(OnAvailableStatusEffectAdded)
                .AddTo(CompositeDisposable);

            _addItemButton.onClick
                .AsObservable()
                .Subscribe(OnAddItemButtonClicked)
                .AddTo(CompositeDisposable);

            _itemsDropdown.onValueChanged
                .AsObservable()
                .Subscribe(OnItemOptionSelected)
                .AddTo(CompositeDisposable);

            _addStatusEffectButton.onClick
                .AsObservable()
                .Subscribe(OnAddStatusEffectButtonClicked)
                .AddTo(CompositeDisposable);

            _statusEffectsDropdown.onValueChanged
                .AsObservable()
                .Subscribe(OnStatusEffectOptionSelected)
                .AddTo(CompositeDisposable);
        }

        private void OnAvailableItemAdded(CollectionAddEvent<ItemType> addEvent)
        {
            _itemsDropdown.options.Add(new TMP_Dropdown.OptionData()
            {
                text = addEvent.Value.ToString()
            });
        }

        private void OnAvailableStatusEffectAdded(CollectionAddEvent<StatusEffectType> addEvent)
        {
            _statusEffectsDropdown.options.Add(new TMP_Dropdown.OptionData()
            {
                text = addEvent.Value.ToString()
            });
        }

        private void OnAddItemButtonClicked(Unit unit)
        {
            ViewModel.AddItem(_selectedItemOptionIndex);
        }

        private void OnAddStatusEffectButtonClicked(Unit unit)
        {
            ViewModel.AddStatusEffect(_selectedStatusEffectIndex);
        }

        private void OnItemOptionSelected(int index)
        {
            _selectedItemOptionIndex = index;
        }

        private void OnStatusEffectOptionSelected(int index)
        {
            _selectedStatusEffectIndex = index;
        }
    }
}