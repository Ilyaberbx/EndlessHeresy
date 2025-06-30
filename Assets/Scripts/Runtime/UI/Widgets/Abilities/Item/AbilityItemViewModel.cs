using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Widgets.Abilities.Item
{
    public sealed class AbilityItemViewModel : BaseViewModel<AbilityItemModel>
    {
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        private const float MaxCooldownProgress = 1f;
        public IReactiveProperty<float> CooldownProgress { get; } = new ReactiveProperty<float>();
        public IReactiveProperty<AbilityState> StateProperty { get; } = new ReactiveProperty<AbilityState>();
        public IReactiveProperty<Sprite> IconProperty { get; } = new ReactiveProperty<Sprite>();

        public AbilityItemViewModel(IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        protected override void Initialize(AbilityItemModel model)
        {
            Model.Ability.ElapsedCooldownTime
                .Subscribe(OnElapsedCooldownTimeChanged)
                .AddTo(CompositeDisposable);

            Model.Ability.State
                .Subscribe(OnStateChanged)
                .AddTo(CompositeDisposable);

            var identifier = Model.Ability.Identifier;
            var configuration = _gameplayStaticDataService.GetAbilityData(identifier);
            IconProperty.Value = configuration.Icon;
        }

        private void OnElapsedCooldownTimeChanged(float elapsedCooldownTime)
        {
            var cooldown = Model.Ability.Cooldown;
            CooldownProgress.Value = MaxCooldownProgress - (elapsedCooldownTime / cooldown);
        }

        private void OnStateChanged(AbilityState state)
        {
            StateProperty.Value = state;
        }
    }
}