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
        [SerializeField] private TMP_Dropdown _affinityDefenseLevelsDropdown;
        [SerializeField] private TMP_Dropdown _damageTypesDropdown;
        [SerializeField] private Button _addStatusEffectButton;
        [SerializeField] private Button _setAffinityLevelButton;

        private int _selectedItemOptionIndex;
        private int _selectedStatusEffectIndex;
        private int _selectedAffinityDefenseIndex;
        private int _selectedDamageTypeIndex;

        protected override void Initialize(CheatsHudViewModel viewModel)
        {
            ViewModel.AvailableItemsProperty
                .ObserveAddWithInitial()
                .Subscribe(OnAvailableItemAdded)
                .AddTo(CompositeDisposable);

            ViewModel.AvailableStatusEffectsProperty
                .ObserveAddWithInitial()
                .Subscribe(OnAvailableStatusEffectAdded)
                .AddTo(CompositeDisposable);

            ViewModel.AffinityDefenseItemsProperty
                .ObserveAddWithInitial()
                .Subscribe(OnAffinityDefenseItemAdded)
                .AddTo(CompositeDisposable);

            ViewModel.DamageTypesProperty
                .ObserveAddWithInitial()
                .Subscribe(OnDamageTypeAdded)
                .AddTo(CompositeDisposable);

            _itemsDropdown.onValueChanged
                .AsObservable()
                .Subscribe(OnItemOptionSelected)
                .AddTo(CompositeDisposable);

            _statusEffectsDropdown.onValueChanged
                .AsObservable()
                .Subscribe(OnStatusEffectOptionSelected)
                .AddTo(CompositeDisposable);

            _affinityDefenseLevelsDropdown.onValueChanged
                .AsObservable()
                .Subscribe(OnAffinityDefenseOptionSelected)
                .AddTo(CompositeDisposable);

            _damageTypesDropdown.onValueChanged
                .AsObservable()
                .Subscribe(OnDamageTypeOptionSelected)
                .AddTo(CompositeDisposable);

            _setAffinityLevelButton.onClick
                .AsObservable()
                .Subscribe(OnSetAffinityLevelButtonClicked)
                .AddTo(CompositeDisposable);

            _addStatusEffectButton.onClick
                .AsObservable()
                .Subscribe(OnAddStatusEffectButtonClicked)
                .AddTo(CompositeDisposable);

            _addItemButton.onClick
                .AsObservable()
                .Subscribe(OnAddItemButtonClicked)
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

        private void OnAffinityDefenseItemAdded(CollectionAddEvent<DefenseLevelType> addEvent)
        {
            _affinityDefenseLevelsDropdown.options.Add(new TMP_Dropdown.OptionData()
            {
                text = addEvent.Value.ToString()
            });
        }

        private void OnDamageTypeAdded(CollectionAddEvent<DamageType> addEvent)
        {
            _damageTypesDropdown.options.Add(new TMP_Dropdown.OptionData()
            {
                text = addEvent.Value.ToString()
            });
        }

        private void OnSetAffinityLevelButtonClicked(Unit unit)
        {
            ViewModel.SetAffinityDefenseLevel(_selectedAffinityDefenseIndex, _selectedDamageTypeIndex);
        }

        private void OnAddItemButtonClicked(Unit unit)
        {
            ViewModel.AddInventoryItem(_selectedItemOptionIndex);
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

        private void OnAffinityDefenseOptionSelected(int index)
        {
            _selectedAffinityDefenseIndex = index;
        }

        private void OnDamageTypeOptionSelected(int index)
        {
            _selectedDamageTypeIndex = index;
        }
    }
}